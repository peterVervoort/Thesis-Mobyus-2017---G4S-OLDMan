using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;

namespace G4S.Models
{
    public class OrderItemHistorySearchModel : SearchModelBase<OrderItemHistory>
    {
        public int? OrderItemId { get; set; }
        public int? StateChangeId { get; set; }
        public int? StateFromId { get; set; }
        public int? StateToId { get; set; }


        public override SearchBase<OrderItemHistory> Map()
        {
            return Mapper.Map<OrderItemHistorySearchCriteria>(this);
        }
    }
}
