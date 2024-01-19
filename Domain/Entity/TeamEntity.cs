using Domain.Entity.Common;
using Util.CustomAttributes;

namespace Domain.Entity
{
    [TableInfo("Team")]
    public class TeamEntity : DefaultEntity
    {
        public string Name { get; set; }
    }

}
