using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class OrderItemHistorySearchCriteria : SearchBase<OrderItemHistory>
    {
        public int? OrderItemId { get; set; }
        public int? StateChangeId { get; set; }
        public int? StateFromId { get; set; }
        public int? StateToId { get; set; }
    }
}
