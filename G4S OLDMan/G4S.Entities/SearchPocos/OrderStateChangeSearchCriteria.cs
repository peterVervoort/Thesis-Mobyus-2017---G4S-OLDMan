using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class OrderStateChangeSearchCriteria : SearchBase<OrderStateChange>
    {
        public int? StateFromId { get; set; }
        public string StateFrom { get; set; }
        public int? StateToId { get; set; }
        public string StateTo { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
}
