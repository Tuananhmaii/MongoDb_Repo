using Microsoft.AspNetCore.Mvc;
using MongoDb_Repo.Domain.Interface.Service;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController(IFileUploadService uploadService) : ControllerBase
    {
        private readonly IFileUploadService _uploadService = uploadService;

        [HttpPost("Evaluation")]
        public async Task<IActionResult> UploadEvaluation(string authorId, List<IFormFile> files)
        {
            var _files = files.Where(f => f.Length > 0).Select(f => new KeyValuePair<string, Stream>(f.FileName, f.OpenReadStream()));
            var result = await _uploadService.HandleEvaluationFiles(_files, authorId);
            return Ok(new { UploadedCounts = files.Count, InsertedCount = result });
        }
    }
}
