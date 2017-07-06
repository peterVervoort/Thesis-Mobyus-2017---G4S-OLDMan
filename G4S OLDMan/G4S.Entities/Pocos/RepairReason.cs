using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class RepairReason : EntityBase
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}

