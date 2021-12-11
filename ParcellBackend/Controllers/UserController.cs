using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParcellBackend.Data.Models;
using ParcellBackend.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcellBackend.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly UserService _userService;

        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetUsers();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id) {
            var user = await _userService.GetUser(id);

            if (user is null) {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser) {

            await _userService.CreateUser(newUser);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, User updatedUser) {
            var user = await _userService.GetUser(id);

            if (user is null) {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateUser(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await _userService.GetUser(id);

            if (user is null) {
                return NotFound();
            }

            await _userService.DeleteUser(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserWithMail(string mail) {

            var user = await _userService.getUserWithMail(mail);

            if(user is null) {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        public async Task<ActionResult> loginWithMail(string mail, string password) {

            var user = await _userService.loginWithMail(mail, password);


            if (user is null) {
                return NotFound();
            }

            return Ok("Kullanıcı girisi onaylandı");
        }


    }
}
