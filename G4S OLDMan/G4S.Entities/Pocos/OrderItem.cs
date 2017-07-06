using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class OrderItem : EntityBase
    {
        public int PurchaseOrderId { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public string CostCenter { get; set; }
        public int QuantityOfProducts { get; set; }
        public virtual ICollection<MobileDevice> MobileDevices { get; set; }
        public virtual ICollection<LoginLicence> LoginLicences { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual ProductType Type { get; set; }

        public int? DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public virtual DeviceType DeviceType { get; set; }

        public DateTime? DeliveryOfSupplier { get; set; }
        public DateTime? DeliveryToOperations { get; set; }
        public DateTime? AnnulationDate { get; set; }
        public virtual ICollection<OrderItemHistory> ItemChanges { get; set; }
    }
}
