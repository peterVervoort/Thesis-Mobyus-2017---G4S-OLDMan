using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    class PurchaseOrderFilter : EntityFilterBase<PurchaseOrder>, IEntityFilter<PurchaseOrder>
    {
        public override Task<IQueryable<PurchaseOrder>> FilterAsync(IQueryable<PurchaseOrder> query, SearchBase<PurchaseOrder> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(PurchaseOrderSearchCriteria)))
            {

                PurchaseOrderSearchCriteria criteria = (PurchaseOrderSearchCriteria)searchCriteria;

                if (criteria.PurchaseOrderNumber.HasValue)
                {
                    query = query.Where(x => x.PurchaseOrderNumber.ToString().Contains(criteria.PurchaseOrderNumber.Value.ToString()));
                }
                if (criteria.OrderDate.HasValue)
                {
                    query = query.Where(x => x.OrderDate == criteria.OrderDate.Value);
                }
            }
            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<PurchaseOrder> Order(IQueryable<PurchaseOrder> query, SearchBase<PurchaseOrder> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "purchaseOrderNumber":
                        query = descending ? query.OrderByDescending(x => x.PurchaseOrderNumber) : query.OrderBy(x => x.PurchaseOrderNumber);
                        break;
                    case "orderDate":
                        query = descending ? query.OrderByDescending(x => x.OrderDate) : query.OrderBy(x => x.OrderDate);
                        break;
                    case "annulationDate":
                        query = descending ? query.OrderByDescending(x => x.AnnulationDate) : query.OrderBy(x => x.AnnulationDate);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }

    }
}