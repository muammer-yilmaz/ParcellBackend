using Microsoft.AspNetCore.Mvc;
using ParcellBackend.Data.Models;
using ParcellBackend.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcellBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerServiceController : ControllerBase
    {
        private readonly CustomerServiceServiceRepository customerserviceService;

        public CustomerServiceController(CustomerServiceServiceRepository customerserviceService)
        {
            this.customerserviceService = customerserviceService;
        }

        [HttpGet]
        public async Task<List<CustomerService>> GetCustomerServices()
        {
            var textList = await customerserviceService.GetList();

            return textList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CustomerService>> GetCustomerService(string id)
        {
            var text = await customerserviceService.Get(id);


            if (text is null)
            {
                return NotFound();
            }

            return text;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerService(CustomerService newCustomerService)
        {

            await customerserviceService.Create(newCustomerService);

            return CreatedAtAction(nameof(GetCustomerService), new { id = newCustomerService.Id }, newCustomerService);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCustomerService(string id, CustomerService updatedCustomerService)
        {
            var text = await customerserviceService.Get(id);

            if (text is null)
            {
                return NotFound();
            }

            updatedCustomerService.Id = text.Id;

            await customerserviceService.Update(id, updatedCustomerService);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCustomerService(string id)
        {
            var text = await customerserviceService.Get(id);

            if (text is null)
            {
                return NotFound();
            }

            await customerserviceService.Delete(id);

            return NoContent();
        }
    }
}
