using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class RepairReasonSearchModel : SearchModelBase<RepairReason>
    {
        public int? StateId { get; set; }
        public string State { get; set; }
        public string Reason { get; set; }

        public override SearchBase<RepairReason> Map()
        {
            return Mapper.Map<RepairReasonSearchCriteria>(this);
        }
    }
}