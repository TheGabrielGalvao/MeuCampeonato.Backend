using Dapper;
using Database;
using Domain.Interface.Repository;
using Util.Helpers;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        protected string GetTableName()
        {
            return EntityHelper.FormatEntityNameForSql<T>("Entity");
        }

        protected string GetTableInfo()
        {
            return EntityHelper.FormatEntityNameForSql<T>("Entity");
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var (tableName, schemaName) = EntityHelper.GetTableInfo<T>();
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>($"SELECT * FROM {schemaName}.{tableName}");
        }

        public virtual async Task<T> GetByIdAsync(Guid uuid)
        {
            var (tableName, schemaName) = EntityHelper.GetTableInfo<T>();
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {schemaName}.{tableName} WHERE Uuid = @Uuid", new { Uuid = uuid });
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var (tableName, schemaName) = EntityHelper.GetTableInfo<T>();
            using var connection = _context.CreateConnection();

            var properties = SqlHelper.GetPropertyNames<T>("Id", "Uuid");
            var columnNames = string.Join(", ", properties);
            var columnValues = string.Join(", ", properties.Select(p => "@" + p));

            var sql = $"INSERT INTO {schemaName}.{tableName} ({columnNames}) VALUES ({columnValues})";

            var result = await connection.ExecuteAsync(sql, entity);
            return result > 0 ? entity : null;
        }



        public virtual async Task<T> UpdateAsync(T entity)
        {
            var (tableName, schemaName) = EntityHelper.GetTableInfo<T>();
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync($"UPDATE {schemaName}.{tableName} SET ... WHERE Uuid = @Uuid", entity);
            return result > 0 ? entity : null;
        }

        public virtual async Task<T> DeleteAsync(Guid uuid)
        {
            var (tableName, schemaName) = EntityHelper.GetTableInfo<T>();
            using var connection = _context.CreateConnection();
            var entity = await GetByIdAsync(uuid);
            if (entity != null)
            {
                var result = await connection.ExecuteAsync($"DELETE FROM {schemaName}.{tableName} WHERE Uuid = @Uuid", new { Uuid = uuid });
                return result > 0 ? entity : null;
            }
            return null;
        }
    }
}
