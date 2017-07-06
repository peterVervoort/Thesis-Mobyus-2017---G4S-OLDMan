using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class UserRoleSearchModel : SearchModelBase<UserRole>
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int? UserRoleGroupId { get; set; }
        public int? NotUserRoleGroupId { get; set; }

        public override SearchBase<UserRole> Map()
        {
            return AutoMapper.Mapper.Map<UserRoleSearchCriteria>(this);
        }
    }
}
