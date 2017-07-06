using System;
using System.ComponentModel.DataAnnotations;
using G4S.Entities.Pocos;

namespace G4S.Models
{
    public class PurchaseOrderPostModel : PostModelBase<PurchaseOrder>
    {
        [Required]
        public long PurchaseOrderNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime? AnnulationDate { get; set; }

    }
}