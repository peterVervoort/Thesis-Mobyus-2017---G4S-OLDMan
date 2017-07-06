using G4S.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Entities.SearchPocos
{
    public class PurchaseOrderSearchCriteria : SearchBase<PurchaseOrder>
    {
        public long? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }

    }
}
