using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class OrderItemHistory : EntityBase
    {
        public int? OrderItemId { get; set; }
        [ForeignKey("OrderItemId")]
        public virtual OrderItem OrderItem { get; set; }
        public int? StateChangeId { get; set; }
        [ForeignKey("StateChangeId")]
        public virtual OrderStateChange StateChange { get; set; }

        public int ChangedById { get; set; }
        [ForeignKey("ChangedById")]
        public virtual User ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
