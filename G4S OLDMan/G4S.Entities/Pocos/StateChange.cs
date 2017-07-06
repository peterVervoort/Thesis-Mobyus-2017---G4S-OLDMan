using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class StateChange : EntityBase
    {
        public int? StateFromId { get; set; }
        [ForeignKey("StateFromId")]
        public virtual State StateFrom { get; set; }
        public int? StateToId { get; set; }
        [ForeignKey("StateToId")]
        public virtual State StateTo { get; set; }
        public bool SystemStateChange { get; set; }
        public virtual ICollection<UserRoleGroup> AcceptedRoleGroups { get; set; }
    }
}
