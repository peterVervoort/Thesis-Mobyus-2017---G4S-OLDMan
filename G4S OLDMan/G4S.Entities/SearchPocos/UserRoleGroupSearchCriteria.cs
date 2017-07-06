using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class UserRoleGroupSearchCriteria : SearchBase<UserRoleGroup>
    {
        public string Name { get; set; }
        public int? StateChangeId { get; set; }
        public int? OrderStateChangeId { get; set; }
        public bool? AutoLinkEveryGroup { get; set; }

    }
}
