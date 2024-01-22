using Domain.DTO;

namespace Domain.Interface.Repository
{
    public interface IChampionshipHistoryRepository
    {
        Task<IList<ChampionshipDetailsDTO>> GetHistory();
        Task<IList<ChampionshipDetailsDTO>> GetHistoryByUserUuid(Guid userUuid);
        Task<IList<ChampionshipDetailsDTO>> GetHistoryByUuid(Guid uuid);
    }
}
