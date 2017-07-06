using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class FlocIdPostModel : PostModelBase<FlocId>
    {
        public int FlocIdNumber { get; set; }
        [Required]
        public int LoginLicenceId { get; set; }
        [Required]
        public int LoginSiteId { get; set; }
        
    }
}


