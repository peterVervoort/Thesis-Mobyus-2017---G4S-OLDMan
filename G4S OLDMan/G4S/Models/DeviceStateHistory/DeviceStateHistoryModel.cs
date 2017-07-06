using G4S.Entities.Pocos;
using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class DeviceStateHistoryModel : ModelBase<DeviceStateHistory>
    {
        public int? MobileDeviceId { get; set; }
        public string Comment { get; set; }
        public int? RepairStateChangeId { get; set; }
        public string StateFrom { get; set; }
        public string StateTo { get; set; }
        public string StateToColorHex { get; set; }
        public DateTime ChangeDate { get; set; }
        public int ChangedById { get; set; }
        public string ChangedByUser { get; set; }
    }
}