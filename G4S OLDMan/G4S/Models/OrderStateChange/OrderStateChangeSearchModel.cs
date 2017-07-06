using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class OrderStateChangeSearchModel : SearchModelBase<OrderStateChange>
    {
        public int? StateFromId { get; set; }
        public string StateFrom { get; set; }
        public int? StateToId { get; set; }
        public string StateTo { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductType { get; set; }


        public override SearchBase<OrderStateChange> Map()
        {
            return Mapper.Map<OrderStateChangeSearchCriteria>(this);
        }
    }
}
