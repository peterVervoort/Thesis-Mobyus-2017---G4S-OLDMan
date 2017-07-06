using G4S.Entities.Pocos;

namespace G4S.Models
{
    public class LanguageModel : ModelBase<Language>
    {
        public string Taal { get; set; }
        public string ShortCode { get; set; }
    }
}