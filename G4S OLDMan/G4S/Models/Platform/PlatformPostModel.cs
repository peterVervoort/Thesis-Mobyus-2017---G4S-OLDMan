using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class PlatformPostModel : CsvListItemPostModelBase<Platform>
    {
        public string Platform { get; set; }
    }
}


