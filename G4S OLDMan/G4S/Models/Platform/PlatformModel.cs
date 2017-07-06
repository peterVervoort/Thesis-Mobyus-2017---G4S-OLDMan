using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class PlatformModel : CsvListItemModelBase<Platform>
    {
        public string Platform { get; set; }
    }
}

