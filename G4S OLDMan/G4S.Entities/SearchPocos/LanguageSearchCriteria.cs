using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class LanguageSearchCriteria : SearchBase<Language>
    {
        public string ShortCode { get; set; }
        public string Taal { get; set; }
    }
}
