using AutoMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;

namespace Service
{
    public abstract class BaseService<TEntity, TRequest, TResponse> : IBaseService<TEntity, TRequest, TResponse>
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TResponse>>(entities);
        }

        public virtual async Task<TResponse> GetByIdAsync(Guid uuid)
        {
            var entity = await _repository.GetByIdAsync(uuid);
            return _mapper.Map<TResponse>(entity);
        }

        public virtual async Task<TResponse> AddAsync(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            var addedEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TResponse>(addedEntity);
        }

        public virtual async Task<TResponse> UpdateAsync(Guid uuid, TRequest request)
        {
            var entity = await _repository.GetByIdAsync(uuid);
            if (entity != null)
            {
                _mapper.Map(request, entity);
                var updatedEntity = await _repository.UpdateAsync(entity);
                return _mapper.Map<TResponse>(updatedEntity);
            }
            return default;
        }

        public virtual async Task DeleteAsync(Guid uuid)
        {
            await _repository.DeleteAsync(uuid);
        }
    }
}
