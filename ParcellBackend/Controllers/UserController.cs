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

            var userCheck = await _userService.GetUserWithMail(newUser.Mail);

            if (userCheck is not null) {
                return BadRequest("Bu mail adresi ile kayıtlı kullanıcı bulunuyor.");
            }

            if (newUser.Phone == "yeni") {
                var phoneNumber = await _userService.AssignPhoneNumber();

                while (!_userService.CheckNumberAvailability(phoneNumber).Result) {
                    phoneNumber = await _userService.AssignPhoneNumber();
                }
                newUser.Phone = phoneNumber;
            }

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

        [HttpPut]
        public async Task<ActionResult> UpdateUserInfo(string userId, string mail, string address) {
            await _userService.UpdateUserInfo(userId, mail, address);
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
        public async Task<ActionResult<User>> LoginWithMail(string mail, string password) {

            var user = await _userService.LoginWithMail(mail, password);

            if (user is null) {
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

        [HttpGet]
        public async Task<ActionResult> ChangeUserPassword(string oldPassword, string newPassword) {
            var response = await _userService.ChangeUserPassword(oldPassword, newPassword);

            if (response == "NotFound")
                return NotFound();
            else
                return Ok();

        }

        [HttpGet]
        public async Task<ActionResult> SetUserPlan(string userId, string planId) {

            await _userService.SetUserPlan(userId, planId);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> DepositUserBalance(string userId, double balance) {

            var oldBalance = await _userService.GetUserBalance(userId);

            if (oldBalance + balance > 9999.99) {
                return BadRequest("Maksimum Bakiye 9999.99");
            }

            await _userService.UpdateUserBalance(userId, oldBalance + balance);

            return Ok();

        }

        [HttpPut]
        public async Task<ActionResult> PayWithBalance(string userId, double price) {

            var currentBalance = await _userService.GetUserBalance(userId);

            if (currentBalance < price) {
                return BadRequest("Bakiye Yetersiz.");
            }

            var newBalance = currentBalance - price;

            await _userService.UpdateUserBalance(userId, newBalance);

            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<double>> GetUserBalance(string userId) {

            var balance = await _userService.GetUserBalance(userId);

            return Ok(balance);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetUserPlan(string userId) {

            var planId = await _userService.GetUserPlan(userId);

            if(planId == null || planId == "") {
                return NotFound("Paketiniz Bulunmamaktadır.");
            }
            else {
                return planId;
            }
        }
    }
}

