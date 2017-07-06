using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class StateSearchCriteria : SearchBase<State>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Kind { get; set; }
        public bool? IsSpare { get; set; }

    }
}
