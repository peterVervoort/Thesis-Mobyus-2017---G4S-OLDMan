using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace G4S.Entities.Pocos
{
    public class MobileDevice : ItemBase
    {
        //public MobileDevice()
        //{
        //    LwpSetting = new LwpSetting();
        //}
        public int? OrderItemId { get; set; }
        [ForeignKey("OrderItemId")]
        public virtual OrderItem OrderItem { get; set; }
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public virtual LwpSetting LwpSetting { get; set; }
        public int? DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public virtual DeviceType Type { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<DeviceStateHistory> RepairChanges { get; set; }
        [NotMapped]
        public DeviceStateHistory LastStateHistory
        {
            get
            {
                return this.RepairChanges?.OrderByDescending(rc => rc.ChangeDate)?.FirstOrDefault();
            }
        }
        
        [NotMapped]
        public TimeSpan TimeInLastState
        {
            get
            {
                if (this.LastStateHistory == null) return default(TimeSpan);
                return DateTime.Now - this.LastStateHistory.ChangeDate;
            }
        }

    }
}
