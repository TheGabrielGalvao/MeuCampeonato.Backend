namespace Domain.DTO
{
    public class ChampionshipRequest
    {
        public Guid? Uuid { get; set; }
        public IList<Guid> Teams { get; set; }
        public Guid UserUuid { get; set; }
    }
}
