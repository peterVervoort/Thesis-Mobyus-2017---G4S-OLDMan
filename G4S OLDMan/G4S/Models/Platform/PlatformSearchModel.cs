using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class PlatformSearchModel : SearchModelBase<Platform>
    {
        public string Platform { get; set; }
        public string CsvSynonyms { get; set; }
        public override SearchBase<Platform> Map()
        {
            return Mapper.Map<PlatformSearchCriteria>(this);
        }
    }
}