using G4S.Entities.Pocos;

namespace G4S.Business.Models
{
    public class ConvertedModel
    {
        public MobileDevice MobileDevice { get; set; }
        public LwpSetting LwpSetting { get; set; }
        public CsvImportModel Original { get; set; }
    }
}
