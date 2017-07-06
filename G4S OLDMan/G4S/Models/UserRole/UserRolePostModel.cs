using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class UserRolePostModel: PostModelBase<UserRole>
    {
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}