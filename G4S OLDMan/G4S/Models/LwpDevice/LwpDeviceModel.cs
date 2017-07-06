using G4S.Entities.Pocos;

namespace G4S.Models.LwpDevice
{
    public class LwpDeviceModel : ModelBase<MobileDevice>
    {
        public MobileDeviceModel MobileDevice { get; set; }
        public LwpSettingModel LwpSetting { get; set; }
    }
}