using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto user)
        {
            //validate request
            var username = user.Username.ToLower();

            if(await _repo.UserExists(username))
                return BadRequest("Username already exist");

            var userToCreate = new User
            {
                UserName = username
            };

            var createdUser = await _repo.Register(userToCreate, user.Password);

            //return CreatedAtRoute()
            return StatusCode(201);
        }
    }
}