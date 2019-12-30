using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsStoreApi.Services;
using SportsStoreApi.Models;
using System.Linq;
using System;
using SportsStoreApi.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace SportsStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Authenticate model)
        {
            Console.WriteLine("Authenticate() -------------------------------");
            Console.WriteLine($"email: {model.Email}, password: {model.Password}");
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                Console.WriteLine("Authenticate(): match not found");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            Console.WriteLine("Authenticate(): success");
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("/users/{id}")]
        public IActionResult Get(int id)
        {
            Console.WriteLine($"userssController.Get({id}) -------------------------------");
            var user = _userService.GetById(id);

            if (user == null)
            {
                Console.WriteLine("match not found");
                return NotFound(id);
            }

            Console.WriteLine("Get(): success");
            return Ok(user);
        }

    }
}
