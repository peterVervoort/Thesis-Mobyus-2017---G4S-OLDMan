using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class ProductTypeSearchModel : SearchModelBase<ProductType>
    {
        public string TypeName { get; set; }
        public bool? DeviceTypeRequired { get; set; }
        public bool? LoginLicenceRequired { get; set; }
        public bool? HasOrderStates { get; set; }

        public override SearchBase<ProductType> Map()
        {
            return Mapper.Map<ProductTypeSearchCriteria>(this);
        }
    }
}