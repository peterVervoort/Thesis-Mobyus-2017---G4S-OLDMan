using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class ProductTypeSearchCriteria : SearchBase<ProductType>
    {
        public string TypeName { get; set; }
        public bool? DeviceTypeRequired { get; set; }
        public bool? LoginLicenceRequired { get; set; }
        public bool? HasOrderStates { get; set; }
    }
}
