using Domain.Enum;

namespace Domain.DTO
{
    public class MatchResponse
    {
        public Guid? Uuid { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamNormalTimeScore { get; set; }
        public int AwayTeamNormalTimeScore { get; set; }
        public int? HomeTeamPenaltyScore { get; set; }
        public int? AwayTeamPenaltyScore { get; set; }
        public string MatchWinnerName { get; set; }
        public EChampionshipStage ChampionshipStage { get; set; }
        public Guid ChampionshipUuid { get; set; }
        
    }
}
