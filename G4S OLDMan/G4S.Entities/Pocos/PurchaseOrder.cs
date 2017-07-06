using System;
using System.Collections.Generic;

namespace G4S.Entities.Pocos
{
    public class PurchaseOrder : EntityBase
    {
        public long PurchaseOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? AnnulationDate { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
