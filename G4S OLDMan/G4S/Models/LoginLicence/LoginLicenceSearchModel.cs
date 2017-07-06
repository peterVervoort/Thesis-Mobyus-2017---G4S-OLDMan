using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class LoginLicenceSearchModel : SearchModelBase<LoginLicence>
    {
        public int? OrderItemId { get; set; }
        public bool? CertificateCreated { get; set; }
        public int? NotOrderItemId { get; set; }
        public int? PlatformId { get; set; }
        public int? LoginSiteId { get; set; }
        public string Platform { get; set; }
        public string LoginSite { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public override SearchBase<LoginLicence> Map()
        {
            return Mapper.Map<LoginLicenceSearchCriteria>(this);
        }
    }
}