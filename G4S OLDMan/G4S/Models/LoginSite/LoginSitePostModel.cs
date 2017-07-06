using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class LoginSitePostModel : CsvListItemPostModelBase<LoginSite>
    {
        public string SiteName { get; set; }
    }
}


