namespace MongoDb_Repo.Domain.Interface
{
    public interface IFileUploadService
    {
        public Task<int> HandleEvaluationFiles(IEnumerable<Stream> files,string authorId);
    }
}
