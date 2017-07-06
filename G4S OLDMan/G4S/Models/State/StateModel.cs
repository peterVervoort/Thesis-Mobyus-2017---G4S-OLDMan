using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class StateModel : ModelBase<State>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int KindId { get; set; }
        public string Kind { get; set; }
        public string ColorHex { get; set; }
        public bool IsSpare { get; set; }
    }
}