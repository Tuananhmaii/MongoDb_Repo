using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb_Repo.Domain.Interface;

namespace MongoDb_Repo.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly IFileUploadService _uploadService;

        public FileUploadController(IFileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost("Evaluation")]
        public async Task<IActionResult> UploadEvaluation(List<IFormFile> files)
        {
            await _uploadService.HandleEvaluationFiles(files.Where(f=>f.Length>0).Select(f => f.OpenReadStream())); 
            return Ok();
        }
    }
}
