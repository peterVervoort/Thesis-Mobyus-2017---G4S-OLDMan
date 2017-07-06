using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class OrderItemSearchModel : SearchModelBase<OrderItem>
    {
        public int? PurchaseOrderId { get; set; }
        public int? ItemLine { get; set; }
        public string CostCenter { get; set; }
        public int? QuantityOfProducts { get; set; }
        public int? TypeId { get; set; }
        public string Type { get; set; }
        public int? DeviceTypeId { get; set; }
        public string DeviceType { get; set; }

        public override SearchBase<OrderItem> Map()
        {
            return Mapper.Map<OrderItemSearchCriteria>(this);
        }
    }
}