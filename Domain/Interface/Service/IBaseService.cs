namespace Domain.Interface.Service
{
    public interface IBaseService<TEntity, TRequest, TResponse>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> GetByIdAsync(Guid uuid);
        Task<TResponse> AddAsync(TRequest request);
        Task<TResponse> UpdateAsync(Guid uuid, TRequest request);
        Task DeleteAsync(Guid uuid);
    }
}
