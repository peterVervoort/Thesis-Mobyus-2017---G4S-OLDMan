using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;

namespace G4S.Models
{
    public class DeviceStateHistorySearchModel : SearchModelBase<DeviceStateHistory>
    {
         public int? MobileDeviceId { get; set; }
        public string Comment { get; set; }
        public int? RepairStateChangeId { get; set; }
        public int? StateFromId { get; set; }
        public int? StateToId { get; set; }
        public int? StateChangeId { get; set; }


        public override SearchBase<DeviceStateHistory> Map()
        {
            return Mapper.Map<DeviceStateHistorySearchCriteria>(this);
        }
    }
}
