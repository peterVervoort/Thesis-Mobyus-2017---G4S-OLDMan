using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class FlocIdSearchCriteria : SearchBase<FlocId>
    {
        public int? LoginSiteId { get; set; }
        public int? FlocIdNumber { get; set; }
    }
}
