using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class Language : EntityBase
    {
        [Required]
        public string Taal { get; set; }

        [Required]
        public string ShortCode { get; set; }

        [ForeignKey("LanguageId")]
        public virtual ICollection<Translation> Translations { get; set; }

        [ForeignKey("LanguageId")]
        public virtual ICollection<User> Users { get; set; }
    }
}
