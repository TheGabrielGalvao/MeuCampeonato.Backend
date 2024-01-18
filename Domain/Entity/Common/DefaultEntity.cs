namespace Domain.Entity.Common
{
    public class DefaultEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; } = Guid.NewGuid();
    }
}
