using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class ToBeTreatedMobileDevice : ItemBase
    {
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public virtual ToBeTreatedLwpSetting LwpSetting { get; set; }
        public int? DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public virtual DeviceType Type { get; set; }
        public string DeviceTypeOriginal { get; set; }
        public string LoginSiteOriginal { get; set; }
        public string PlatformOriginal { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Collection<ValidationWarning> Warnings { get; set; }

    }
}
