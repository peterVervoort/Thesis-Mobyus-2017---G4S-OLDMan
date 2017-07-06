using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class UserRoleSearchCriteria : SearchBase<UserRole>
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int? UserRoleGroupId { get; set; }
        public int? NotUserRoleGroupId { get; set; }

    }
}
