using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class LoginSiteSearchModel : SearchModelBase<LoginSite>
    {
        public string SiteName { get; set; }
        public int? UserId { get; set; }
        public int? NotUserId { get; set; }

        public override SearchBase<LoginSite> Map()
        {
            return Mapper.Map<LoginSiteSearchCriteria>(this);
        }
    }
}