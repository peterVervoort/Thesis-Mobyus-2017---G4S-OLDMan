using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace G4S.Business.Services
{
    public class CsvService : ICsvService
    {

        public FileInfo GetCSV<TModel>(IEnumerable<TModel> records)
        {
            string fileName = Path.GetTempFileName();
            using (TextWriter writer = File.CreateText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(records);
            }
            return new FileInfo(fileName);
        }
        
        public IEnumerable<TModel> ReadCSV<TModel>(string fileName, char delimiter = ',')
        {
            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.WillThrowOnMissingField = false;
                csv.Configuration.IsHeaderCaseSensitive = false;
                csv.Configuration.Delimiter = delimiter.ToString();

                return csv.GetRecords<TModel>().ToList();
            }
        }
        
    }
}
