using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Services.Interfaces.User;

namespace SmartHomeAutomation.Api.Controllers.User
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>List of users in JSON format</returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(userService.GetAll());
        }

        /// <summary>
        /// Get an individual user
        /// </summary>
        /// <param name="guid">User GUID</param>
        /// <returns>User object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetUser([FromRoute] Guid guid)
        {
            var user = userService.GetByGuid(guid);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="guid">User GUID from URL</param>
        /// <param name="user">User object from body</param>
        /// <returns>Updated user object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutUser([FromRoute] Guid guid, [FromBody] Domain.Models.UserModels.User user)
        {
            if (guid != user.AccountId)
            {
                return BadRequest();
            }

            return Ok(userService.Upsert(user, User));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">User object from body</param>
        /// <returns>Newly created user object in JSON format</returns>
        [HttpPost]
        public IActionResult PostUser([FromBody] Domain.Models.UserModels.User user)
        {
            return Ok(userService.Upsert(user, User));
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="guid">User GUID from URL</param>
        /// <returns>Deleted user object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteUser([FromRoute] Guid guid)
        {
            return Ok(userService.SoftDelete(guid, User));
        }
    }
}