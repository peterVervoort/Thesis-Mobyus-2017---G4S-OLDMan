using G4S.Entities.Pocos;
using System;

namespace G4S.Models
{
    public class PurchaseOrderModel : ModelBase<PurchaseOrder>
    {
        public long PurchaseOrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? AnnulationDate { get; set; }
    }
}