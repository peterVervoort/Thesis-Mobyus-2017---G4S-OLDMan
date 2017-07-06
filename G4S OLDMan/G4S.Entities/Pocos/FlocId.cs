using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class FlocId : EntityBase
    {
        public int LoginSiteId { get; set; }
        [ForeignKey("LoginSiteId")]
        public virtual LoginSite LoginSite { get; set; }

        public int LoginLicenceId { get; set; }
        [ForeignKey("LoginLicenceId")]
        public virtual LoginLicence LoginLicence { get; set; }

        public int FlocIdNumber { get; set; }
    }
}
