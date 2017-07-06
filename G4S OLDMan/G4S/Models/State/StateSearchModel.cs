using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class StateSearchModel : SearchModelBase<State>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Kind { get; set; }
        public bool? IsSpare { get; set; }


        public override SearchBase<State> Map()
        {
            return Mapper.Map<StateSearchCriteria>(this);
        }
    }
}
