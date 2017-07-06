using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models.LwpDevice
{
    public class LwpDevicePostModel : PostModelBase<MobileDevice>
    {
        [Required]
        public MobileDevicePostModel MobileDevice { get; set; }
        [Required]
        public LwpSettingPostModel LwpSetting { get; set; }
    }
}