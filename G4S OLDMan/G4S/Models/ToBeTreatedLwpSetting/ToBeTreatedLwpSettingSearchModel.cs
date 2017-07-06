using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class ToBeTreatedLwpSettingSearchModel : SearchModelBase<ToBeTreatedLwpSetting>
    {
        public override SearchBase<ToBeTreatedLwpSetting> Map()
        {
            return Mapper.Map<ToBeTreatedLwpSettingSearchCriteria>(this);
        }
    }
}
