using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class MobileDeviceSearchModel : SearchModelBase<MobileDevice>
    {
        public int? OrderItemId { get; set; }
        public int? NotOrderItemId { get; set; }
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public string TypeName { get; set; }
        public int? DeviceTypeId { get; set; }
        public string TagName { get; set; }
        public string LoginSite { get; set; }
        public string StateName { get; set; }
        public bool? SpareState { get; set; }

        public override SearchBase<MobileDevice> Map()
        {
            return Mapper.Map<MobileDeviceSearchCriteria>(this);
        }
    }
}
