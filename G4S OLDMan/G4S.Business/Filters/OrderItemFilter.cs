using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class OrderItemFilter : EntityFilterBase<OrderItem>, IEntityFilter<OrderItem>
    {
        public override Task<IQueryable<OrderItem>> FilterAsync(IQueryable<OrderItem> query, SearchBase<OrderItem> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(OrderItemSearchCriteria)))
            {
                OrderItemSearchCriteria criteria = (OrderItemSearchCriteria)searchCriteria;
                
                if (criteria.PurchaseOrderId.HasValue)
                {
                    query = query.Where(x => x.PurchaseOrderId == criteria.PurchaseOrderId.Value);
                }
                if (criteria.QuantityOfProducts.HasValue)
                {
                    query = query.Where(x => x.QuantityOfProducts == criteria.QuantityOfProducts.Value);
                }
                if (criteria.DeviceTypeId.HasValue)
                {
                    query = query.Where(x => x.DeviceTypeId == criteria.DeviceTypeId);
                }
                if (!string.IsNullOrEmpty(criteria.DeviceType))
                {
                    query = query.Where(x => x.DeviceType.TypeName.Contains(criteria.DeviceType));
                }
                if (!string.IsNullOrEmpty(criteria.Type))
                {
                    query = query.Where(x => x.Type.TypeName.Contains(criteria.Type));
                }
                if (criteria.TypeId.HasValue)
                {
                    query = query.Where(x => x.TypeId == criteria.TypeId.Value);
                }
                if (!string.IsNullOrWhiteSpace(criteria.CostCenter))
                {
                    query = query.Where(x => x.CostCenter == criteria.CostCenter);
                }       
            }
            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<OrderItem> Order(IQueryable<OrderItem> query, SearchBase<OrderItem> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    
                    case "purchaseOrderNumber":
                        query = descending ? query.OrderByDescending(x => x.PurchaseOrder.PurchaseOrderNumber).ThenBy(x => x.Id) : query.OrderBy(x => x.PurchaseOrder.PurchaseOrderNumber).ThenByDescending(x => x.Id);
                break;
                    case "quantityOfProducts":
                        query = descending ? query.OrderByDescending(x => x.QuantityOfProducts) : query.OrderBy(x => x.QuantityOfProducts);
                        break;
                    case "type":
                        query = descending ? query.OrderByDescending(x => x.Type.TypeName) : query.OrderBy(x => x.Type.TypeName);
                        break;
                    case "deviceType":
                        query = descending ? query.OrderByDescending(x => x.DeviceType.TypeName) : query.OrderBy(x => x.DeviceType.TypeName);
                        break;
                    case "canceled":
                        query = descending ? query.OrderByDescending(x => x.AnnulationDate) : query.OrderBy(x => x.AnnulationDate);
                        break;
                    case "supplied":
                        query = descending ? query.OrderByDescending(x => x.DeliveryOfSupplier) : query.OrderBy(x => x.DeliveryOfSupplier);
                        break;
                    default:
                        break;
                }
            } else
            {
                query = query.OrderBy(x => x.PurchaseOrder.PurchaseOrderNumber).ThenBy(x => x.Id);
            }
            return query;

        }
    }
}