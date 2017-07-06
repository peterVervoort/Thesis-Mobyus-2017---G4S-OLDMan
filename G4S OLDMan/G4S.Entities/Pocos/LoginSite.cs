using System.Collections.Generic;

namespace G4S.Entities.Pocos
{
    public class LoginSite : CsvListItem
    {
        public string SiteName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
