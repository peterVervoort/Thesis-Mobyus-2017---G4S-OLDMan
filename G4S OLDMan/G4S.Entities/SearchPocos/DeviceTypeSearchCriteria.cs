using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class DeviceTypeSearchCriteria : SearchBase<DeviceType>
    {
        public string TypeName { get; set; }
        public string CsvSynonyms { get; set; }
        public bool? LwpSettingPossible { get; set; }
    }
}
