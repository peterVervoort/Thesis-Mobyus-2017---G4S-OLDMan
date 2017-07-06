namespace G4S.Entities.SearchPocos
{
    public class ToBeTreatedMobileDeviceSearchCriteria : SearchBase<Pocos.ToBeTreatedMobileDevice>
    {
        public string DeviceName { get; set; }
        public string Reference { get; set; }
        public string Type { get; set; }
        public int? DeviceTypeId { get; set; }
        public string TagName { get; set; }
        public string LoginSite { get; set; }
    }
}
