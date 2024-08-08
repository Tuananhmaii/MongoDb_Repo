namespace MongoDb_Repo.Domain.Interface.Service
{
    public interface IFileUploadService
    {
        public Task<int> HandleEvaluationFiles(IEnumerable<KeyValuePair<string,Stream>> files,string authorId);
    }
}
