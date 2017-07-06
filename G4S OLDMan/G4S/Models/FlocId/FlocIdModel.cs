using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class FlocIdModel : ModelBase<FlocId>
    {
        public int FlocIdNumber { get; set; }
        public string LoginSite { get; set; }
        public int LoginLicenceId { get; set; }
        public int LoginSiteId { get; set; }
    }
}

