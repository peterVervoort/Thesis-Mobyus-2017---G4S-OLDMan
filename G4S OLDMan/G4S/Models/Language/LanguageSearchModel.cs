using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class LanguageSearchModel : SearchModelBase<Language>
    {
        public string ShortCode { get; set; }
        public string Taal { get; set; }

        public override SearchBase<Language> Map()
        {
            return Mapper.Map<LanguageSearchCriteria>(this);
        }
    }
}
