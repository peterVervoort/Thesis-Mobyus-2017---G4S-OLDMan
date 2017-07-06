using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class LwpSettingSearchModel : SearchModelBase<LwpSetting>
    {
        public override SearchBase<LwpSetting> Map()
        {
            return Mapper.Map<LwpSettingSearchCriteria>(this);
        }
    }
}
