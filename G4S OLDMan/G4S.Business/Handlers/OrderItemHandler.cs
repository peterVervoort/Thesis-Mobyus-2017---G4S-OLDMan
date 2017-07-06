using G4S.Business.Repositories;
using G4S.Entities.Pocos;
using System;
using System.Threading.Tasks;
using G4S.Business.Writers;
using G4S.Business.Helpers;
using Microsoft.Practices.Unity;

namespace G4S.Business.Handlers
{
    internal class OrderItemHandler : IOrderItemHandler
    {

        [Dependency]
        public IReader<OrderItem> OrderItemReader { get; set; }
        [Dependency]
        public IWriter<OrderItem> OrderItemWriter { get; set; }
        [Dependency]
        public IWriter<PurchaseOrder> PurchaseOrderWriter { get; set; }
        [Dependency]
        public IReader<PurchaseOrder> PurchaseOrderReader { get; set; }


        public async Task<EntityResult<OrderItem>> CancelOrderItem(int id)
        {
            var item = await OrderItemReader.GetById(id);
            if (item == null) return new EntityResult<OrderItem>(ResultCode.Failed);
            item.AnnulationDate = DateTime.Now;
            var result = await OrderItemWriter.UpdateAsync(item);
            if (result.Code != ResultCode.Success) return result;

            //check all items cancelled
            var count = await OrderItemReader.SearchCount(oi => oi.PurchaseOrderId == item.PurchaseOrderId
                                                                && !oi.AnnulationDate.HasValue);
            if (count == 0)
            {
                var po = await PurchaseOrderReader.GetById(item.PurchaseOrderId);
                po.AnnulationDate = DateTime.Now;
                await PurchaseOrderWriter.UpdateAsync(po);
            }

            return result;
        }

    }

}

