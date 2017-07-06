using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class PlatformSearchCriteria : SearchBase<Platform>
    {
        public string PlatformName { get; set; }
        public string CsvSynonyms { get; set; }
    }
}
