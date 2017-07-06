using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class DeviceTypePostModel : CsvListItemPostModelBase<DeviceType>
    {
        [Required]
        public string TypeName { get; set; }
        public bool LwpSettingPossible { get; set; }
    }
}


