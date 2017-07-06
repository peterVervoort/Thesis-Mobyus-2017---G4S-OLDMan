using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class UserFilter : EntityFilterBase<User>, IEntityFilter<User>
    {
        public override Task<IQueryable<User>> FilterAsync(IQueryable<User> query, SearchBase<User> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(UserSearchCriteria)))
            {

                UserSearchCriteria criteria = (UserSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Email))
                {
                    query = query.Where(x => x.Email.Contains(criteria.Email));
                }

                if (!string.IsNullOrWhiteSpace(criteria.FirstName))
                {
                    query = query.Where(x => x.FirstName.Contains(criteria.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    query = query.Where(x => x.Name.Contains(criteria.Name));
                }

                if (!string.IsNullOrWhiteSpace(criteria.RoleGroup))
                {
                    query = query.Where(x => x.RoleGroup.Name.Contains(criteria.RoleGroup));
                }

                if (!string.IsNullOrWhiteSpace(criteria.RoleName))
                {
                    query = query.Where(x => x.RoleGroup.Roles.Any(r => r.RoleName.Contains(criteria.RoleName)));
                }

                if (!string.IsNullOrWhiteSpace(criteria.SiteName))
                {
                    query = query.Where(x => x.LoginSites.Any(r => r.SiteName.Contains(criteria.SiteName)));
                }

                if (criteria.LoginSiteId.HasValue)
                {
                    query = query.Where(x => x.LoginSites.Any(r => r.Id == criteria.LoginSiteId.Value));
                }

                if (!string.IsNullOrEmpty(criteria.Language))
                {
                    query = query.Where(x => x.Language.Taal.Contains(criteria.Language));
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<User> Order(IQueryable<User> query, SearchBase<User> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "firstName":
                        query = descending ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                        break;
                    case "name":
                        query = descending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case "email":
                        query = descending ? query.OrderByDescending(x => x.Email) : query.OrderBy(x => x.Email);
                        break;
                    case "language":
                        query = descending ? query.OrderByDescending(x => x.Language.Taal) : query.OrderBy(x => x.Language.Taal);
                        break;
                    case "roleGroup":
                        query = descending ? query.OrderByDescending(x => x.RoleGroup.Name) : query.OrderBy(x => x.RoleGroup.Name);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
