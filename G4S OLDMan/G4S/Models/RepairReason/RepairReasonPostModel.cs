using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class RepairReasonPostModel : PostModelBase<RepairReason>
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public int StateId { get; set; }

    }
}


