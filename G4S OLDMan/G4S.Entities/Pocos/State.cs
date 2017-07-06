using G4S.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class State : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int KindId { get; set; }
        [ForeignKey("KindId")]
        public virtual StateKind Kind { get; set; }
        public string ColorHex { get; set; }
        public bool IsSpare { get; set; }
    }
}
