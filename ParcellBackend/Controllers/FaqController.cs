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
    public class FaqController : ControllerBase
    {
        private readonly FaqServiceRepository faqService;

        public FaqController(FaqServiceRepository faqService)
        {
            this.faqService = faqService;
        }

        [HttpGet]
        public async Task<List<Faq>> GetFaqs()
        {
            var faqList = await faqService.GetList();

            return faqList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Faq>> GetFaq(string id)
        {
            var faq = await faqService.Get(id);


            if (faq is null)
            {
                return NotFound();
            }

            return faq;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaq(Faq newFaq)
        {

            await faqService.Create(newFaq);

            return CreatedAtAction(nameof(GetFaq), new { id = newFaq.Id }, newFaq);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateFaq(string id, Faq updatedFaq)
        {
            var faq = await faqService.Get(id);

            if (faq is null)
            {
                return NotFound();
            }

            updatedFaq.Id = faq.Id;

            await faqService.Update(id, updatedFaq);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteFaq(string id)
        {
            var faq = await faqService.Get(id);

            if (faq is null)
            {
                return NotFound();
            }

            await faqService.Delete(id);

            return NoContent();
        }
    }
}
