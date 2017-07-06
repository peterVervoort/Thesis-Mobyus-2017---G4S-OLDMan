using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class UserRoleModel: ModelBase<UserRole>
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool SystemRole { get; set; }
    }
}