using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System;
using Microsoft.Practices.Unity;
using G4S.Business.Repositories;
using System.Collections.Generic;

namespace G4S.Business.Writers
{
    public class PurchaseOrderWriter : Writer<PurchaseOrder>, IWriter<PurchaseOrder>
    {
        [Dependency]
        public IWriter<OrderItem> OrderItemWriter { get; set; }
        [Dependency]
        public IReader<OrderItem> OrderItemReader { get; set; }

        public override async Task<EntityResult<PurchaseOrder>> DeleteAsync(int id)
        {
            var result = await base.DeleteAsync(id);
            if (result.Code ==  ResultCode.Success)
            {
                var orderItems = await OrderItemReader.Search(oi => oi.PurchaseOrderId == id);
                foreach (var item in orderItems)
                {
                    await OrderItemWriter.DeleteAsync(item.Id);
                }
            }
            return result;
        }

    }
}
