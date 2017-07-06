using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class TranslationSearchModel : SearchModelBase<Translation>
    {
        public string TaalShortCode { get; set; }
        public string Language { get; set; }
        public string Group { get; set; }
        public string Value { get; set; }
        public string Keyword { get; set; }

        public override SearchBase<Translation> Map()
        {
            return Mapper.Map<TranslationSearchCriteria>(this);
        }
    }
}
