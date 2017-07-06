using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System;

namespace G4S.Business.Writers
{
    public class OrderItemHistoryWriter : Writer<OrderItemHistory>, IWriter<OrderItemHistory>
    {

        public override async Task<EntityResult<OrderItemHistory>> InsertAsync(OrderItemHistory entity)
        {
            if (entity != null)
            {
                entity.ChangeDate = DateTime.Now;
                var userid = await SecurityService.GetCurrentUserId();
                if (!userid.HasValue) throw new UnauthorizedAccessException();
                entity.ChangedById = userid.Value;
            }
            return await base.InsertAsync(entity);
        }

    }
}
