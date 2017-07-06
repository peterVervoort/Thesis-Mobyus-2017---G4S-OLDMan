using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class UserSearchModel : SearchModelBase<User>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string RoleGroup { get; set; }
        public string SiteName { get; set; }
        public int? LoginSiteId { get; set; }
        public string Language { get; set; }

        public override SearchBase<User> Map()
        {
            return Mapper.Map<UserSearchCriteria>(this);
        }
    }
}
