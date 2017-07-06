using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System.Threading.Tasks;

namespace G4S.Business.Handlers
{
    public interface IOrderItemHandler
    {
        Task<EntityResult<OrderItem>> CancelOrderItem(int id);
    }
}