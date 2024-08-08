using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;
using MongoDb_Repo.Infrastructure.Data;

namespace MongoDb_Repo.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // /// <summary>
        // /// Retrieves a user by their ID.
        // /// </summary>
        // /// <param name="id"> ID of user.</param>
        // /// <returns>The user object if found; otherwise, NotFound.</returns>
        // [HttpGet("{id}")]
        // [SwaggerOperation(Summary = "Retrieve a user by ID", Description = "Gets a user by their ID from the database.")]
        // [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<IActionResult> Get(string id)
        // {
        //     var user = await _userRepository.GetAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(user);
        // }

        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="email">Email address of user.</param>
        /// <returns>The user object if found; otherwise, NotFound.</returns>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <returns>The created user object with its ID.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">ID of user.</param>
        /// <param name="user">User object.</param>
        /// <returns>No content if the update was successful; otherwise, BadRequest or NotFound.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(string id, [FromBody] User user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _userRepository.GetAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userRepository.UpdateAsync(id, user);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">ID of user.</param>
        /// <returns>No content if the deletion was successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.RemoveAsync(id);
            return NoContent();
        }
    }
}
