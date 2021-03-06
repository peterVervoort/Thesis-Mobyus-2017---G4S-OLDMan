﻿using G4S.Entities.Pocos;
using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class MobileDevicePostModel : PostModelBase<MobileDevice>
    {
        public int? OrderItemId { get; set; }
        [Required]
        public int PlatformId { get; set; }
        public string DeviceName { get; set; }
        [Required]
        public string Reference { get; set; }
        public string PhoneNumber { get; set; }
        public int? LoginSiteId { get; set; }
        [Required]
        public int DeviceTypeId { get; set; }
    }
}