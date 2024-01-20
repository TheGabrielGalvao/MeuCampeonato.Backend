using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Service
{
    public interface IMatchService : IBaseService<MatchEntity, MatchRequest, MatchResponse>
    {
    }
}
