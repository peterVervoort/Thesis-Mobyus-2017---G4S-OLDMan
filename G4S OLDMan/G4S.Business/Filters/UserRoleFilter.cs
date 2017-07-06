using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class UserRoleFilter : EntityFilterBase<UserRole>, IEntityFilter<UserRole>
    {
        public override Task<IQueryable<UserRole>> FilterAsync(IQueryable<UserRole> query, SearchBase<UserRole> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(UserRoleSearchCriteria))) {

                UserRoleSearchCriteria criteria = (UserRoleSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.RoleName))
                {
                    query = query.Where(x => x.RoleName.Contains(criteria.RoleName));
                }

                if (!string.IsNullOrWhiteSpace(criteria.Description))
                {
                    query = query.Where(x => x.Description.Contains(criteria.Description));
                }

                if (criteria.UserRoleGroupId.HasValue)
                {
                    query = query.Where(x => x.Groups.Any(g => g.Id == criteria.UserRoleGroupId.Value));
                }

                if (criteria.NotUserRoleGroupId.HasValue)
                {
                    query = query.Where(x => !x.Groups.Any(g => g.Id == criteria.NotUserRoleGroupId.Value));
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<UserRole> Order(IQueryable<UserRole> query, SearchBase<UserRole> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "roleName":
                        query = descending ? query.OrderByDescending(x => x.RoleName) : query.OrderBy(x => x.RoleName);
                        break;
                    case "description":
                        query = descending ? query.OrderByDescending(x => x.Description) : query.OrderBy(x => x.Description);
                        break;
                    default:
                        break;
                }
            }
            return base.Order(query, searchCriteria);
        }

    }
}
