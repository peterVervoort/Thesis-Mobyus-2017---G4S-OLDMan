using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4S.Entities.Pocos
{
    public class UserRole : EntityBase
    {
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool SystemRole { get; set; }

        public virtual ICollection<UserRoleGroup> Groups { get; set; }
        public virtual ICollection<StateChange> RepairStateChanges { get; set; }

    }
}
