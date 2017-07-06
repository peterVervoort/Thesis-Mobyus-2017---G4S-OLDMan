using G4S.Entities.Pocos;
using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class ToBeTreatedMobileDeviceModel : ToBeTreatedModelBase<ToBeTreatedMobileDevice>
    {
        public long? PurchaseOrderNumber { get; set; }
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public int LwpSettingId { get; set; }
        public int? DeviceTypeId { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public int? PlatformId { get; set; }
        public string Platform { get; set; }
        public string Country { get; set; }
        public int LoginSiteId { get; set; }
        public string LoginSite { get; set; }
        public bool HasLwpSetting { get; set; }
        public string DeviceTypeOriginal { get; set; }
        public string LoginSiteOriginal { get; set; }
        public string PlatformOriginal { get; set; }
    }
}

