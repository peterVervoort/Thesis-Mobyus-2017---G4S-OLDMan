using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class StateChangeSearchModel : SearchModelBase<StateChange>
    {
        public int? StateFromId { get; set; }
        public string StateFrom { get; set; }
        public int? StateToId { get; set; }
        public string StateTo { get; set; }
        public bool? SystemStateChange { get; set; }

        public override SearchBase<StateChange> Map()
        {
            return Mapper.Map<StateChangeSearchCriteria>(this);
        }
    }
}
