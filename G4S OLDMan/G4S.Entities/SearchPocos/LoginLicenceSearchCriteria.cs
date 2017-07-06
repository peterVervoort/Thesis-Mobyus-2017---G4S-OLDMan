using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class LoginLicenceSearchCriteria : SearchBase<LoginLicence>
    {
        public int? OrderItemId { get; set; }
        public int? NotOrderItemId { get; set; }
        public int? PlatformId { get; set; }
        public string Platform { get; set; }
        public int? LoginSiteId { get; set; }
        public string LoginSite { get; set; }
        public bool? CertificateCreated { get; set; }
        public string PurchaseOrderNumber { get; set; }
    }
}
