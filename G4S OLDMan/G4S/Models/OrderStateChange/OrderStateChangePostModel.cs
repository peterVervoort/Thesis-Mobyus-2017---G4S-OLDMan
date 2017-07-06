using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class OrderStateChangePostModel : PostModelBase<OrderStateChange>
    {
        public int? StateFromId { get; set; }
        [Required]
        public int StateToId { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
    }
}