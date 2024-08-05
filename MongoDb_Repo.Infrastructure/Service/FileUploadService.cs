using ExcelDataReader;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;
using System.Data;

namespace MongoDb_Repo.Infrastructure.Service
{
    public class FileUploadService(IEvaluationFileRepository evaluationFileRepository) : IFileUploadService
    {
        private readonly string SheetToRead = "EvaluationChecklist";
        private readonly IEvaluationFileRepository _evaluationFileRepository = evaluationFileRepository;
        private static readonly ExcelDataSetConfiguration excelConfigs = new()
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                EmptyColumnNamePrefix = "#EMPTY_HEADER@",
                UseHeaderRow = true,
            }
        };


        public async Task<int> HandleEvaluationFiles(IEnumerable<Stream> files, string authorId)
        {
            //var evaluations = new List<EvaluationFile>();
            foreach (var file in files)
            {
                using var reader = ExcelReaderFactory.CreateReader(file);
                var dataset = reader.AsDataSet(excelConfigs).Tables[SheetToRead];
                if (dataset == null)
                    continue;

                var properties = new List<EvaluationProperties>();
                var columns = dataset.Columns;
                var rows = dataset.Rows;
                columns[0].ColumnName = "Category";
                columns[1].ColumnName = "Description";

                for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
                {
                    if (IsEmptyRow(rows, rowIndex))
                        continue;

                    if (IsCategorySubItemRow(rows, properties, rowIndex))
                    {
                       
                    }
                    else
                    {
                       

                    }
                }
               

            }
            

            return 0;
            
        }

        private static EvaluationPropertySubitems GetFromCells(DataRowCollection rows, DataColumnCollection columns, int rowIndex)
        {
            return new()
            {
                Description = rows[rowIndex]["Description"].ToString(),
                Items = columns.Cast<DataColumn>()
                                           .Select(x => x.ColumnName).AsEnumerable()
                                           //Skip first 2 columns which are "Categories" & "Description"
                                           .Skip(2)
                                           .Select(col => new KeyValuePair<string, string>(col, rows[rowIndex][col]?.ToString() ?? string.Empty))
                                           .ToList()
            };
        }

        private static bool IsCategorySubItemRow(DataRowCollection rows, List<EvaluationProperties> properties, int rowIndex)
        {
            return string.IsNullOrWhiteSpace(rows[rowIndex]["Category"].ToString()) && properties.Count > 0;
        }

        private static bool IsEmptyRow(DataRowCollection rows, int rowIndex)
        {
            return rows[rowIndex].ItemArray.All(c => string.IsNullOrWhiteSpace(c?.ToString()));
        }
    }
}
