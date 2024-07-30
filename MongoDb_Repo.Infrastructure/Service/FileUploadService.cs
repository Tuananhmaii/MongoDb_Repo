using ExcelDataReader;
using MongoDb_Repo.Domain.Interface;
using System.IO;

namespace MongoDb_Repo.Infrastructure.Service
{
    public class FileUploadService : IFileUploadService
    {
        private static readonly ExcelDataSetConfiguration excelReaderConfigs = new()
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                UseHeaderRow = true
            }
        };

        public async Task HandleEvaluationFiles(IEnumerable<Stream> files)
        {
            foreach (var file in files) 
            {
                using var reader = ExcelReaderFactory.CreateReader(file);
                var dataset = reader.AsDataSet(excelReaderConfigs).Tables[1];
                dataset.Dispose();

            }

            return;
        }
    }
}
