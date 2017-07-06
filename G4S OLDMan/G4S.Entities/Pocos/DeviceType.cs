using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Entities.Pocos
{
    public class DeviceType : CsvListItem
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool LwpSettingPossible { get; set; }
    }
}
