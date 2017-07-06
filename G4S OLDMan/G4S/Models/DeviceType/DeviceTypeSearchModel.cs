using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class DeviceTypeSearchModel : SearchModelBase<DeviceType>
    {
        public string TypeName { get; set; }
        public bool? LwpSettingPossible { get; set; }
        public string CsvSynonyms { get; set; }

        public override SearchBase<DeviceType> Map()
        {
            return Mapper.Map<DeviceTypeSearchCriteria>(this);
        }
    }
}