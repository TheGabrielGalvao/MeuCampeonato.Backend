using Domain.Entity.Common;

namespace Domain.Entity
{
    internal class MatchEntity : DefaultEntity
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int TournamentStage { get; set; }
        public int TournamentId { get; set; }
        public int TournamentUserId { get; set; }
    }
}
