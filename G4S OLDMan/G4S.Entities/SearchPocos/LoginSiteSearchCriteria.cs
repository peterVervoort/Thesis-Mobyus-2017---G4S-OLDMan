using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class LoginSiteSearchCriteria : SearchBase<LoginSite>
    {
        public string SiteName { get; set; }
        public int? UserId { get; set; }
        public int? NotUserId { get; set; }
        public string CsvSynonyms { get; set; }

    }
}
