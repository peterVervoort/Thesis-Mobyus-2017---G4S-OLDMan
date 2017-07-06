using G4S.Entities.Pocos;
using System;

namespace G4S.Models
{
    public class OrderItemModel : ModelBase<OrderItem>
    {
        public long PurchaseOrderNumber { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ItemLine { get; set; }
        public string CostCenter { get; set; }
        public int QuantityOfProducts { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int DeviceTypeId { get; set; }
        public string DeviceType { get; set; }
        public DateTime? DeliveryOfSupplier { get; set; }
        public DateTime? DeliveryToOperations { get; set; }
        public DateTime? AnnulationDate { get; set; }
        public string CurrentState { get; set; }
        public DateTime? LastStateDate { get; set; }
        public bool Supplied { get { return DeliveryOfSupplier.HasValue; } }
        public bool Canceled { get { return AnnulationDate.HasValue; } }
    }
}