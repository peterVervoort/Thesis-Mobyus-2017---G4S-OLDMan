using System.ComponentModel.DataAnnotations;

namespace G4S.Entities.Pocos
{
    public class ProductType : EntityBase
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool DeviceTypeRequired { get; set; }
        [Required]
        public bool LoginLicenceRequired { get; set; }
        [Required]
        public bool HasOrderStates { get; set; }
    }
}