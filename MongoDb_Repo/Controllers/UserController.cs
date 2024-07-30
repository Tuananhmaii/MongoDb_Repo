using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDb_Repo.Infrastructure.Data;

namespace MongoDb_Repo.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public UsersController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                var users = _context.Users.Find(_ => true).ToList();
                return Ok("Successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
