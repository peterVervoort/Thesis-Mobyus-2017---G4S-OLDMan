using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class UserSearchCriteria : SearchBase<User>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string RoleGroup { get; set; }
        public int? LoginSiteId { get; set; }
        public string SiteName { get; set; }
        public string Language { get; set; }
    }
}
