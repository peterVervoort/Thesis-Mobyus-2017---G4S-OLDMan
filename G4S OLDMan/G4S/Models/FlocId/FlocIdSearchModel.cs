using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class FlocIdSearchModel : SearchModelBase<FlocId>
    {
        public int? LoginSiteId { get; set; }
        public int? FlocIdNumber { get; set; }

        public override SearchBase<FlocId> Map()
        {
            return Mapper.Map<FlocIdSearchCriteria>(this);
        }
    }
}