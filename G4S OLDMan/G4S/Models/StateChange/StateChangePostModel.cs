using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class StateChangePostModel : PostModelBase<StateChange>
    {
        public int? StateFromId { get; set; }
        [Required]
        public int StateToId { get; set; }
    }
}