using Microsoft.AspNetCore.Mvc;
using ParcellBackend.Data.Models;
using ParcellBackend.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcellBackend.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase {

        private readonly UserServiceRepository userService;

        public HomeController(UserServiceRepository userService) {

            this.userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers() =>
            await userService.GetList();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetUser(string id) {
            var user = await userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser) {

            await userService.Create(newUser);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateUser(string id, User updatedUser) {
            var user = await userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await userService.Update(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteUser(string id) {
            var user = await userService.Get(id);

            if (user is null) {
                return NotFound();
            }

            await userService.Delete(id);

            return NoContent();
        }


    }
}
