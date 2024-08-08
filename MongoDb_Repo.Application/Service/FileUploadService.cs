using MongoDb_Repo.Domain.Interface.Repository;
using MongoDb_Repo.Domain.Interface.Service;

namespace MongoDb_Repo.Infrastructure.Service
{
    public class FileUploadService(
        IExcelService excelService,
        IUserSkillRepository userSkillRepository,
        ISkillPropertiesRepository skillPropertiesRepository
    ) : IFileUploadService
    {
        private readonly IExcelService _excelService = excelService;
        private readonly IUserSkillRepository _userSkillRepository = userSkillRepository;
        private readonly ISkillPropertiesRepository _skillPropertiesRepository = skillPropertiesRepository;

        public async Task<int> HandleEvaluationFiles(IEnumerable<KeyValuePair<string, Stream>> files, string authorId)
        {
            var (skillList, propertiesList, filesExtracted) = _excelService.ExtractEvaluationData(files, authorId);
            await _userSkillRepository.AddManyAsync(skillList);
            await _skillPropertiesRepository.AddManyAsync(propertiesList);
            return filesExtracted;
        }

    }
}
