using Domain.Entity.Common;
using Domain.Enum;

namespace Domain.Entity
{
    internal class MatchEntity : DefaultEntity
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamNormalTimeScore { get; set; }
        public int AwayTeamNormalTimeScore { get; set; }
        public int? HomeTeamPenaltyScore { get; set; }
        public int? AwayTeamPenaltyScore { get; set; }
        public int MatchWinnerId { get; set; }
        public EChampionshipStage TournamentStage { get; set; }
        public int TournamentId { get; set; }
        public int TournamentUserId { get; set; }
    }
}
