using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class UserRoleGroupPostModel : PostModelBase<UserRoleGroup>
    {
        [Required]
        public string Name { get; set; }
        
        public bool AutoLinkEveryGroup { get; set; }
    }
}