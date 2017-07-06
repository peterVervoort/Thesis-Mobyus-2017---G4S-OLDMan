using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class RepairReasonModel : ModelBase<RepairReason>
    {
        public string Reason { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
    }
}

