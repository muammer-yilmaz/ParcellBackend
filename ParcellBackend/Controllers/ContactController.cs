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
    public class ContactController : Controller
    {
        private readonly ContactServiceRepository contactService;

        public ContactController(ContactServiceRepository contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<List<Contact>> GetContacts()
        {
            var textList = await contactService.GetList();

            return textList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            var text = await contactService.Get(id);


            if (text is null)
            {
                return NotFound();
            }

            return text;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact newContact)
        {

            await contactService.Create(newContact);

            return CreatedAtAction(nameof(GetContact), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateContact(string id, Contact updatedContact)
        {
            var text = await contactService.Get(id);

            if (text is null)
            {
                return NotFound();
            }

            updatedContact.Id = text.Id;

            await contactService.Update(id, updatedContact);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var text = await contactService.Get(id);

            if (text is null)
            {
                return NotFound();
            }

            await contactService.Delete(id);

            return NoContent();
        }
    }
}
