using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class DeviceStateHistorySearchCriteria : SearchBase<DeviceStateHistory>
    {
        public int? MobileDeviceId { get; set; }
        public string Comment { get; set; }
        public int? RepairStateChangeId { get; set; }
        public int? StateFromId { get; set; }
        public int? StateToId { get; set; }
    }
}
