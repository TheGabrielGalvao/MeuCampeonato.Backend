using Domain.Enum;

namespace Domain.DTO
{
    public class ChampionshipDetailsDTO
    {
        public Guid UserUuid { get; set; }
        public string Username { get; set; }
        public Guid ChampionshipUuid { get; set; }
        public string Name { get; set; }
        public Guid MatchUuid { get; set; }
        public Guid HomeTeamUuid { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamNormalTimeScore { get; set; }
        public int HomeTeamPenaltyScore { get; set; }
        public Guid AwayTeamUuid { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamNormalTimeScore { get; set; }
        public int AwayTeamPenaltyScore { get; set; }
        public Guid MatchWinnerUuid { get; set; }
        public string MatchWinnerName { get; set; }
        public EChampionshipStage ChampionshipStage { get; set; }
    }
}
