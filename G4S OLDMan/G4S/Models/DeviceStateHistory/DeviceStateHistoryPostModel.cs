using G4S.Entities.Pocos;
using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class DeviceStateHistoryPostModel : PostModelBase<DeviceStateHistory>
    {
        [Required]
        public int? MobileDeviceId { get; set; }
        public string Comment { get; set; }
        public int? RepairStateChangeId { get; set; }
    }
}