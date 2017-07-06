using System.Collections.Generic;
using System.Threading.Tasks;
using G4S.Business.Models;

namespace G4S.Business.Handlers
{
    public interface ICsvHandler
    {
        Task HandleImportRecords(IEnumerable<CsvImportModel> importModels);
        Task HandleToBeTreated(int id);
    }
}