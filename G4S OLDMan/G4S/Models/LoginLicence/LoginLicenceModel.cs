using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class LoginLicenceModel : ModelBase<LoginLicence>
    {
        public int? OrderItemId { get; set; }
        public string LoginSite { get; set; }
        public long? PurchaseOrderNumber { get; set; }
        public string Platform { get; set; }
        public bool CertificateCreated { get; set; }
        public int FlocIdCount { get; set; }
    }
}

