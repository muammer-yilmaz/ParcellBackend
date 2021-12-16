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
    public class DeviceController : ControllerBase {

        private readonly DeviceServiceRepository deviceService;

        public DeviceController(DeviceServiceRepository deviceService) {
            this.deviceService = deviceService;
        }

        [HttpGet]
        public async Task<List<Device>> GetDevices() {
            var userList = await deviceService.GetList();

            return userList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Device>> GetDevice(string id) {
            var user = await deviceService.Get(id);


            if (user is null) {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(Device newDevice) {

            await deviceService.Create(newDevice);

            return CreatedAtAction(nameof(GetDevice), new { id = newDevice.Id }, newDevice);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateDevice(string id, Device updatedDevice) {
            var user = await deviceService.Get(id);

            if (user is null) {
                return NotFound();
            }

            updatedDevice.Id = user.Id;

            await deviceService.Update(id, updatedDevice);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteDevice(string id) {
            var user = await deviceService.Get(id);

            if (user is null) {
                return NotFound();
            }

            await deviceService.Delete(id);

            return NoContent();
        }



    }
}
