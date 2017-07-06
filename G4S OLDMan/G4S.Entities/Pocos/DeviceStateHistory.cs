using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class DeviceStateHistory : EntityBase
    {

        public int? MobileDeviceId { get; set; }
        [ForeignKey("MobileDeviceId")]
        public virtual MobileDevice MobileDevice { get; set; }
        public string Comment { get; set; }
        public int? RepairStateChangeId { get; set; }
        [ForeignKey("RepairStateChangeId")]
        public virtual StateChange RepairStateChange { get; set; }

        public int ChangedById { get; set; }
        [ForeignKey("ChangedById")]
        public virtual User ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}

