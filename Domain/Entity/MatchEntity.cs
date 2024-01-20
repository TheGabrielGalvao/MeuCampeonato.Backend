using Domain.Entity.Common;
using Domain.Enum;
using Util.CustomAttributes;

namespace Domain.Entity
{
    [TableInfo("Match")]
    public class MatchEntity : DefaultEntity
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamNormalTimeScore { get; set; }
        public int AwayTeamNormalTimeScore { get; set; }
        public int? HomeTeamPenaltyScore { get; set; }
        public int? AwayTeamPenaltyScore { get; set; }
        public int MatchWinnerId { get; set; }
        public EChampionshipStage ChampionshipStage { get; set; }
        public int ChampionshipId { get; set; }
    }
}
