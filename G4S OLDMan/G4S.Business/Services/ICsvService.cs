using System.Collections.Generic;
using System.IO;
using G4S.Entities.Pocos;

namespace G4S.Business.Services
{
    public interface ICsvService
    {
        FileInfo GetCSV<TModel>(IEnumerable<TModel> records);
        IEnumerable<TModel> ReadCSV<TModel>(string fileName, char delimiter = ',');
    }
}