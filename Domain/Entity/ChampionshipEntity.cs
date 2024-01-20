using Domain.Entity.Common;
using Util.CustomAttributes;

namespace Domain.Entity
{
    [TableInfo("Championship")]
    public class ChampionshipEntity : DefaultEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
