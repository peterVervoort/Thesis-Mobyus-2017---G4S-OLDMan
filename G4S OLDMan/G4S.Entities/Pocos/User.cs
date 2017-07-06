using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class User : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }


        //Navigation properties

        public int? RoleGroupId { get; set; }
        [ForeignKey("RoleGroupId")]
        public virtual UserRoleGroup RoleGroup { get; set; }


        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }


        public virtual ICollection<LoginSite> LoginSites { get; set; }

    }
}
