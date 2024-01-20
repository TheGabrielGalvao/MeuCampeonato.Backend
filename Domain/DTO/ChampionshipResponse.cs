namespace Domain.DTO
{
    public class ChampionshipResponse
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public UserResponse User { get; set; }
        public IList<MatchResponse>? Matches { get; set; }
    }
}
