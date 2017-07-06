using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Handlers
{
    class OrderItemStateHistoryHandler : IOrderItemStateHistoryHandler
    {
        [Dependency]
        public IReader<OrderStateChange> OrderItemStateChangeReader { get; set; }
        [Dependency]
        public IReader<OrderItem> OrderItemReader { get; set; }
        [Dependency]
        public ISecurityService SecurityService { get; set; }

        public async Task<IEnumerable<OrderStateChange>> GetPossibleStateChanges(int orderItemId)
        {
            var currentUser = await SecurityService.GetCurrentUser();
            if (currentUser == null) throw new UnauthorizedAccessException();
            
            var orderItem = await OrderItemReader.GetById(orderItemId, nameof(OrderItem.ItemChanges));
            var stateFromId = orderItem.ItemChanges?.OrderBy(rc => rc.ChangeDate).LastOrDefault()?.StateChange?.StateToId;

            var possibleStates = await OrderItemStateChangeReader.Search(sc => sc.StateFromId == stateFromId
                                    && (!sc.AcceptedRoleGroups.Any() || sc.AcceptedRoleGroups.Any(arg => arg.Id == currentUser.RoleGroupId))
                                    && sc.ProductTypeId == orderItem.TypeId);

            return possibleStates;

        }
    }
}
