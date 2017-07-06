namespace G4S.Entities.SearchPocos
{
    public class MobileDeviceSearchCriteria : SearchBase<Pocos.MobileDevice>
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
    }
}
