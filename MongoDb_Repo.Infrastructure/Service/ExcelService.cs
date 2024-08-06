using ExcelDataReader;
using MongoDb_Repo.Domain.Models;
using MongoDb_Repo.Infrastructure.Extension;
using MongoDb_Repo.Infrastructure.Interface;
using System.Data;

namespace MongoDb_Repo.Infrastructure.Service
{
    public class ExcelService : IExcelService
    {
        private readonly string EvaluationSheetName = "EvaluationChecklist";
        private readonly string[] ColumnNames = [
            "Skills",
            "Skill-Properties",
            "Details",
            "Advance level requirements",
            "Expert level requirements",
            "Required For Level",
            "Weight",
            "Upgraded?",
        ];
        private readonly Dictionary<string, LevelRequirement> LevelRequirements = new()
        {
            {"Rookie",LevelRequirement.Rookie},
            {"Junior",LevelRequirement.Junior},
            {"Middle",LevelRequirement.Middle},
            {"Senior",LevelRequirement.Senior},
            {string.Empty,LevelRequirement.Unknown},
        };

        private static readonly ExcelDataSetConfiguration excelConfigs = new()
        {
            ConfigureDataTable = _ => new ExcelDataTableConfiguration
            {
                EmptyColumnNamePrefix = "#EMPTY_HEADER@",
                UseHeaderRow = true,
            }
        };

        private IEnumerable<KeyValuePair<string, string>> GetFromCells(DataRow row, DataColumnCollection columns)
        {
            var Items = columns.Cast<DataColumn>()
                               .Select(cell => cell.ColumnName)
                               .Where(col => !ColumnNames.Contains(col))
                               .AsEnumerable()
                               .Select(col => new KeyValuePair<string, string>(col, row[col]?.ToString() ?? string.Empty));

            return Items;
        }

        public (IEnumerable<UserSkill> skillList, IEnumerable<SkillProperty> propertiesList,int filesExtracted) ExtractEvaluationData(IEnumerable<KeyValuePair<string, Stream>> files, string authorId)
        {
            var skillList = new List<UserSkill>();
            var propertiesList = new List<SkillProperty>();
            var filesExtracted = 0;

            foreach (var file in files)
            {
                using var reader = ExcelReaderFactory.CreateReader(file.Value);
                var dataset = reader.AsDataSet(excelConfigs).Tables[EvaluationSheetName];
                if (dataset == null)
                    continue;

                var columns = dataset.Columns;
                var columnNames = new List<string>();
                columns[0].ColumnName = ColumnNames[0];
                columns[1].ColumnName = ColumnNames[1];
                foreach (var col in columns)
                {
                    columnNames.Add(col?.ToString() ?? string.Empty);
                }
                var isValidColumns = ColumnNames.All(col=>columnNames.Contains(col));

                if (!isValidColumns)
                    continue;

                var skills = new List<UserSkill>();
                var properties = new List<SkillProperty>();
                var rows = dataset.Rows;
                var skillId = string.Empty;


                for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
                {
                    var row = rows[rowIndex];
                    var hasSkill = row.HasValueOnColumn(ColumnNames[0]);
                    var hasSkillProperty = row.HasValueOnColumn(ColumnNames[1]);
                    if (hasSkill)
                    {
                        var skill = new UserSkill()
                        {
                            AuthorId = authorId,
                            SkillName = row[ColumnNames[0]].ToString(),
                            FromFile = file.Key,
                            SkillProperties = []
                        };
                        skillId = skill.Id;
                        skills.Add(skill);
                    }
                    if (hasSkillProperty && skillId != string.Empty)
                    {
                        var property = new SkillProperty()
                        {
                            SkillId = skillId,
                            Description = row[ColumnNames[1]].ToString(),
                            Details = row[ColumnNames[2]].ToString(),
                            AdvancedLevelRequirements = row[ColumnNames[3]].ToString(),
                            ExpertLevelRequirements = row[ColumnNames[4]].ToString(),
                            LevelRequirements = LevelRequirements[row[ColumnNames[5]].ToString() ?? string.Empty],
                            Weight = Int32.Parse(row[ColumnNames[6]].ToString() ?? "0"),
                            Upgraded = row[ColumnNames[7]].ToString()?.ToUpper() == "YES",
                            Others = GetFromCells(row, columns).ToList()
                        };
                        properties.Add(property);
                    }
                }
                skills.ForEach(skill=> { skill.Created = DateTime.UtcNow; });
                skillList.AddRange(skills);
                propertiesList.AddRange(properties);
                filesExtracted ++;
            }

            return (skillList, propertiesList,filesExtracted);
        }
    }
}
