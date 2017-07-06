using G4S.Entities.Pocos;
using System.Collections.Generic;

namespace G4S.Entities.SearchPocos
{
    public class OrderItemSearchCriteria : SearchBase<OrderItem>
    {
        public int? PurchaseOrderId { get; set; }
        public int? ItemLine { get; set; }
        public string CostCenter { get; set; }
        public int? QuantityOfProducts { get; set; }
        public int? TypeId { get; set; }
        public string Type { get; set; }
        public int? DeviceTypeId { get; set; }
        public string DeviceType { get; set; }

    }
}
