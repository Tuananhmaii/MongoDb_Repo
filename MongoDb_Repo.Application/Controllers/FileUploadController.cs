using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MongoDb_Repo.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost("Evaluation")]
        public async Task<IActionResult> UploadEvaluation(List<IFormFile> files)
        {
            return Ok();
        }
    }
}
