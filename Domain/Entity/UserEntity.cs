using Domain.Entity.Common;

namespace Domain.Entity
{
    public class UserEntity : DefaultEntity
    {
        public string Username { get; set; }
        public string UserPass { get; set; }
    }
}
