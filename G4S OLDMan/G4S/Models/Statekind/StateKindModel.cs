using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class StateKindModel : ModelBase<State>
    {
        public string Name { get; set; }
    }
}