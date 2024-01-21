using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Service
{
    public interface IChampionshipService : IBaseService<ChampionshipEntity, ChampionshipRequest, ChampionshipResponse>
    {
        Task<IList<ChampionshipDetailsDTO>> GetChampionshipDetailsByUuid(Guid uuid);

        Task<IList<ChampionshipDetailsDTO>> GetChampionshipDetailsByUserUuid(Guid userUuid);
    }
}
