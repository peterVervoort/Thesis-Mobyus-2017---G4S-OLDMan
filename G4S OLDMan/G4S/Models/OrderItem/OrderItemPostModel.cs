using System.ComponentModel.DataAnnotations;
using G4S.Entities.Pocos;
using System;

namespace G4S.Models
{
    public class OrderItemPostModel : PostModelBase<OrderItem>
    {
        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int ItemLine { get; set; }

        [Required]
        public string CostCenter { get; set; }

        [Required]
        public int QuantityOfProducts { get; set; }
        [Required]
        public int TypeId { get; set; }
        public int? DeviceTypeId { get; set; }
        public DateTime? DeliveryOfSupplier { get; set; }
        public DateTime? DeliveryToOperations { get; set; }
        public DateTime? AnnulationDate { get; set; }
    }
}