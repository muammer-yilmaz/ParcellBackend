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

        private readonly UserServiceRepository _userService;

        public UserController(UserServiceRepository userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await _userService.GetList();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id) {
            var user = await _userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser) {

            await _userService.Create(newUser);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, User updatedUser) {
            var user = await _userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.Update(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await _userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            await _userService.Delete(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<User>> LoginWithMail(string mail , string password) {

            var user = await _userService.LoginWithMail(mail,password);

            if(user is null) {
                return NotFound("kullanıcı bulunamadı");
            }

            return user;
        }

        
        [HttpGet]
        public async Task<ActionResult<User>> GetUserWithMail(string mail) {

            var user = await _userService.GetUserWithMail(mail);


            if (user is null) {
                return NotFound();
            }

            return Ok(user);
        }
        

    }
}
