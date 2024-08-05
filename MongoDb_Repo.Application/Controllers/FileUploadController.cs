using Microsoft.AspNetCore.Mvc;
using MongoDb_Repo.Domain.Interface;

namespace MongoDb_Repo.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController(IFileUploadService uploadService) : ControllerBase
    {
        private readonly IFileUploadService _uploadService = uploadService;

        [HttpPost("Evaluation")]
        public async Task<IActionResult> UploadEvaluation(string authorId, List<IFormFile> files)
        {
            var result = await _uploadService.HandleEvaluationFiles(files.Where(f => f.Length > 0).Select(f => f.OpenReadStream()), authorId);
            return Ok(new { UploadedCounts = files.Count, InsertedCount = result });
        }
    }
}
