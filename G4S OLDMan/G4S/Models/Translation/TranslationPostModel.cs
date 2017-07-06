using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class TranslationPostModel : PostModelBase<Translation>
    {
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public string Keyword { get; set; }
        [Required]
        public string Value { get; set; }
    }
}