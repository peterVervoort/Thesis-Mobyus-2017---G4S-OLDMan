using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class LoginSiteModel : CsvListItemModelBase<LoginSite>
    {
        public string SiteName { get; set; }
    }
}

