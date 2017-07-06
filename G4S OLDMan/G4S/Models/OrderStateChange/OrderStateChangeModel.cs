using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class OrderStateChangeModel : ModelBase<OrderStateChange>
    {
        public int? StateFromId { get; set; }
        public string StateFrom { get; set; }

        public int StateToId { get; set; }
        public string StateTo { get; set; }

        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
}