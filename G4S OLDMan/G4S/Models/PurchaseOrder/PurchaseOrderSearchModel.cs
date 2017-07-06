using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;

namespace G4S.Models
{
    public class PurchaseOrderSearchModel : SearchModelBase<PurchaseOrder>
    {
        public long? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }

        public override SearchBase<PurchaseOrder> Map()
        {
            return Mapper.Map<PurchaseOrderSearchCriteria>(this);
        }
    }
}
