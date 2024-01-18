namespace Domain.DTO
{
    public class UserRequest
    {
        public Guid? Uuid { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
    }
}
