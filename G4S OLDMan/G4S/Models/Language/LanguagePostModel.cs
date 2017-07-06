using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class LanguagePostModel : PostModelBase<Language>
    {
        [Required]
        public string Taal { get; set; }
        [Required]
        public string ShortCode { get; set; }
    }
}