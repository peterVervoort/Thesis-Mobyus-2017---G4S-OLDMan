using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Entities.Pocos
{
    public class LoginLicence : ItemBase
    {
        public int? OrderItemId { get; set; }
        [ForeignKey("OrderItemId")]
        public virtual OrderItem OrderItem { get; set; }
        public virtual ICollection<FlocId> FlocIds { get; set; }
        public bool CertificateCreated { get; set; }

    }
}
