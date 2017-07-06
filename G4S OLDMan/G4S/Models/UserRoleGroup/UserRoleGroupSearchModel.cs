using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class UserRoleGroupSearchModel : SearchModelBase<UserRoleGroup>
    {
        public string Name { get; set; }
        public int? StateChangeId { get; set; }
        public int? OrderStateChangeId { get; set; }
        public bool? AutoLinkEveryGroup { get; set; }

        public override SearchBase<UserRoleGroup> Map()
        {
            return Mapper.Map<UserRoleGroupSearchCriteria>(this);
        }
    }
}
