using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G4S.Models.DeviceReplacements
{
    public class DeviceReplacementPostModel
    {
        [Required]
        public int OldMobileDeviceId { get; set; }
        [Required]
        public int NewMobileDeviceId { get; set; }
        [Required]
        public int OldStateId { get; set; }
        [Required]
        public int NewStateId { get; set; }
    }
}