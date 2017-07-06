using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class StateChangeSearchCriteria : SearchBase<StateChange>
    {
        public int? StateFromId { get; set; }
        public string StateFrom { get; set; }
        public int? StateToId { get; set; }
        public string StateTo { get; set; }
        public bool? SystemStateChange { get; set; }
    }
}
