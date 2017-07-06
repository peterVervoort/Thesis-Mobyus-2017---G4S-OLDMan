using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class RepairStateChangeModel : ModelBase<StateChange>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}