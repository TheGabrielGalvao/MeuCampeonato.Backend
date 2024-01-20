using Domain.Enum;

namespace Domain.DTO
{
    public class MatchRequest
    {
        public Guid? Uuid { get; set; }
        public TeamRequest HomeTeam { get; set; }
        public TeamRequest AwayTeam { get; set; }
        public int HomeTeamNormalTimeScore { get; set; }
        public int AwayTeamNormalTimeScore { get; set; }
        public int? HomeTeamPenaltyScore { get; set; }
        public int? AwayTeamPenaltyScore { get; set; }
        public Guid MatchWinnerUuid { get; set; }
        public EChampionshipStage ChampionshipStage { get; set; }
        public Guid ChampionshipUuid { get; set; }
        public Guid UserUuid { get; set; }
    }
}
