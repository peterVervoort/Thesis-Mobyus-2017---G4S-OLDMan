using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class TranslationSearchCriteria : SearchBase<Translation>
    {
        public string TaalShortCode { get; set; }
        public string Language { get; set; }
        public string Group { get; set; }
        public string Value { get; set; }
        public string Keyword { get; set; }
    }
}
