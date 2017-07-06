using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class ToBeTreatedMobileDeviceSearchModel : SearchModelBase<ToBeTreatedMobileDevice>
    {
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public int? DeviceTypeId { get; set; }
        public string TagName { get; set; }
        public string LoginSite { get; set; }

        public override SearchBase<ToBeTreatedMobileDevice> Map()
        {
            return Mapper.Map<ToBeTreatedMobileDeviceSearchCriteria>(this);
        }
    }
}