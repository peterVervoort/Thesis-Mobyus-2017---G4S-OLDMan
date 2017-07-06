using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class RepairReasonSearchCriteria : SearchBase<RepairReason>
    {
        public string Reason { get; set; }
        public int? StateId { get; set; }
        public string State { get; set; }
    }
}
