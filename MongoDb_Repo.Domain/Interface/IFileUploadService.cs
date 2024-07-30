namespace MongoDb_Repo.Domain.Interface
{
    public interface IFileUploadService
    {
        public Task HandleEvaluationFiles(IEnumerable<Stream> files);
    }
}
