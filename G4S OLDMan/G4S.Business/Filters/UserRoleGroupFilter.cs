using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class UserRoleGroupFilter : EntityFilterBase<UserRoleGroup>, IEntityFilter<UserRoleGroup>
    {
        public override Task<IQueryable<UserRoleGroup>> FilterAsync(IQueryable<UserRoleGroup> query, SearchBase<UserRoleGroup> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(UserRoleGroupSearchCriteria))) {

                UserRoleGroupSearchCriteria criteria = (UserRoleGroupSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    query = query.Where(x => x.Name.Contains(criteria.Name));
                }

                if (criteria.StateChangeId.HasValue)
                {
                    query = query.Where(x => x.StateChanges.Any(sc => sc.Id == criteria.StateChangeId.Value));
                }

                if (criteria.OrderStateChangeId.HasValue)
                {
                    query = query.Where(x => x.OrderStateChanges.Any(sc => sc.Id == criteria.OrderStateChangeId.Value));
                }

                if (criteria.AutoLinkEveryGroup.HasValue)
                {
                    query = query.Where(x => x.AutoLinkEveryGroup == criteria.AutoLinkEveryGroup.Value);
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<UserRoleGroup> Order(IQueryable<UserRoleGroup> query, SearchBase<UserRoleGroup> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "name":
                        query = descending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case "autoLinkEveryGroup":
                        query = descending ? query.OrderByDescending(x => x.AutoLinkEveryGroup) : query.OrderBy(x => x.AutoLinkEveryGroup);
                        break;
                    default:
                        break;
                }
            }
            return base.Order(query, searchCriteria);
        }

    }
}
