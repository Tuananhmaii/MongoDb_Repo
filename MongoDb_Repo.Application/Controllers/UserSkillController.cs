using Microsoft.AspNetCore.Mvc;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb_Repo.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillController : ControllerBase
    {
        private readonly IUserSkillRepository _userSkillRepository;

        public UserSkillController(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userSkills = await _userSkillRepository.GetAllAsync();
            return Ok(userSkills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var userSkill = await _userSkillRepository.GetAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }
            return Ok(userSkill);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSkillsByUserId(string userId)
        {
            var userSkills = await _userSkillRepository.GetSkillsByUserId(userId);
            if (userSkills == null || !userSkills.Any())
            {
                return NotFound();
            }
            return Ok(userSkills);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserSkill userSkill)
        {
            if (userSkill == null)
            {
                return BadRequest();
            }

            await _userSkillRepository.AddAsync(userSkill);
            return CreatedAtAction(nameof(Get), new { id = userSkill.Id }, userSkill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserSkill userSkill)
        {
            if (userSkill == null || id != userSkill.Id)
            {
                return BadRequest();
            }

            var existingUserSkill = await _userSkillRepository.GetAsync(id);
            if (existingUserSkill == null)
            {
                return NotFound();
            }

            await _userSkillRepository.UpdateAsync(id, userSkill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userSkill = await _userSkillRepository.GetAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }

            await _userSkillRepository.RemoveAsync(id);
            return NoContent();
        }
    }
}
