using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G4S.Entities.Pocos
{
    public class UserRoleGroup : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AutoLinkEveryGroup { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<StateChange> StateChanges { get; set; }
        public virtual ICollection<OrderStateChange> OrderStateChanges { get; set; }
    }
}
