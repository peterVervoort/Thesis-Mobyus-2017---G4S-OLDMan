using System.Collections.Generic;
using System.Threading.Tasks;
using G4S.Entities.Pocos;

namespace G4S.Business.Handlers
{
    public interface IOrderItemStateHistoryHandler
    {
        Task<IEnumerable<OrderStateChange>> GetPossibleStateChanges(int orderItemId);
    }
}