using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class TranslationModel : ModelBase<Translation>
    {
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string Group { get; set; }
        public string Keyword { get; set; }
        public string Value { get; set; }
    }
}