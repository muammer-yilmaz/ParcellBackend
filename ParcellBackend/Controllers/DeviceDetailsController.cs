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
    public class DeviceDetailsController : Controller {

        private readonly DeviceDetailsServiceRepository deviceDetailsService;

        public DeviceDetailsController(DeviceDetailsServiceRepository deviceDetailsService) {
            this.deviceDetailsService = deviceDetailsService;
        }

        [HttpGet]
        public async Task<List<DeviceDetails>> GetDeviceDetails() {
            var textList = await deviceDetailsService.GetList();

            return textList;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceDetails(DeviceDetails newDeviceDetails) {

            await deviceDetailsService.Create(newDeviceDetails);

            return CreatedAtAction(nameof(GetDeviceDetails), new { id = newDeviceDetails.Id }, newDeviceDetails);
        }

        [HttpGet]
        public async Task<ActionResult<DeviceDetails>> GetDeviceDetail(string deviceId) {

            var detail = await deviceDetailsService.GetDeviceDetail(deviceId);

            if(detail is null) {
                return NotFound();
            }

            return Ok(detail);
        }

        [HttpGet]
        public async Task<ActionResult<Phone>> GetPhoneDetail(string deviceId) {
            var detail = await deviceDetailsService.GetPhoneDetail(deviceId);
            return Ok(detail);
        }

        [HttpGet]
        public async Task<ActionResult<Headphone>> GetHeadphoneDetail(string deviceId) {
            var detail = await deviceDetailsService.GetHeadphoneDetail(deviceId);
            return Ok(detail);
        }

        [HttpGet]
        public async Task<ActionResult<Powerbank>> GetPowerbankDetail(string deviceId) {
            var detail = await deviceDetailsService.GetPowerbankDetail(deviceId);
            return Ok(detail);
        }

    }
}
