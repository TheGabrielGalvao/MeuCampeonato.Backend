using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;

namespace Service
{
    public class MatchService : BaseService<MatchEntity, MatchRequest, MatchResponse>, IMatchService
    {
        public MatchService(IMatchRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
