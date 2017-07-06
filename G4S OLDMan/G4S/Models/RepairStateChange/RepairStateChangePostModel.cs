using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class RepairStateChangePostModel : PostModelBase<StateChange>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}