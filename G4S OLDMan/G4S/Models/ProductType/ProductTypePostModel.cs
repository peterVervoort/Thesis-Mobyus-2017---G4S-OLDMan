using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class ProductTypePostModel : PostModelBase<ProductType>
    {
        [Required]
        public string TypeName { get; set; }
        public bool DeviceTypeRequired { get; set; }
        public bool LoginLicenceRequired { get; set; }
        public bool HasOrderStates { get; set; }
    }
}


