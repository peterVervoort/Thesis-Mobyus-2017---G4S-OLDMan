using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class LoginLicencePostModel : PostModelBase<LoginLicence>
    {
        public int? OrderItemId { get; set; }
        public bool CertificateCreated { get; set; }
        public int? PlatformId { get; set; }
        public int? LoginSiteId { get; set; }
    }
}


