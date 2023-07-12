using Microsoft.AspNetCore.Mvc;
using WebAPIv1.Domain.Contracts.Users;
using WebAPIv1.Domain.Models;
using WebAPIv1.Services.Users;

namespace WebAPIv1.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            User newUser = new User(
                Guid.NewGuid(),
                request.UserName,
                request.Password,
                DateTime.Now.ToShortDateString());

            _userService.CreateUser(newUser);

            UserResponse response = new UserResponse(newUser.UserId,
                newUser.UserName,
                newUser.Password,
                newUser.CreatedAt);

            return CreatedAtAction(
                actionName: nameof(GetUserByUsername),
                routeValues: new { username = newUser.UserName },
                value: response
                );
        }

        [HttpGet(Name = "/User/{username:string}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            User retrievedUser = await _userService.GetUser(username);

            if (retrievedUser is not null)
            {
                UserResponse response = new UserResponse(
                    retrievedUser.UserId,
                    retrievedUser.UserName,
                    retrievedUser.Password,
                    retrievedUser.CreatedAt);

                return Ok(response);
            }

            return NotFound();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            User retrievedUser = await _userService.GetUser(request.UserName);
            if(retrievedUser is not null)
            {
                _userService.UpdateUserAsync(request);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete(Name = "/User/{id:guid}")]
        public IActionResult DeleteUser(Guid UserId)
        {
            _userService.DeleteUserAsync(UserId);
            return NoContent();
        }
    }
}
