using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class ItemBase : EntityBase
    {
        public int? PlatformId { get; set; }
        [ForeignKey("PlatformId")]
        public virtual Platform Platform { get; set; }
        public string Country { get; set; } = "Belgium";
        public int? LoginSiteId { get; set; }
        [ForeignKey("LoginSiteId")]
        public virtual LoginSite LoginSite { get; set; }
    }
}
