using Domain.Entity.Common;
using Util.CustomAttributes;

namespace Domain.Entity
{
    [TableInfo("Users", "auth")]
    public class UserEntity : DefaultEntity
    {
        public string UserName { get; set; }
        public string UserPass { get; set; }
    }
}
