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
    public class InvoiceController : Controller {

        private readonly InvoiceServiceRepository invoiceService;

        public InvoiceController(InvoiceServiceRepository invoiceService) {
            this.invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<List<Invoice>> GetInvoices() {
            var textList = await invoiceService.GetList();

            return textList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Invoice>> GetInvoice(string id) {
            var text = await invoiceService.Get(id);


            if (text is null) {
                return NotFound();
            }

            return text;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(Invoice newInvoice) {

            await invoiceService.Create(newInvoice);

            return CreatedAtAction(nameof(GetInvoice), new { id = newInvoice.Id }, newInvoice);
        }

        [HttpPost]
        public async Task<ActionResult> AddInvoice(string userId, string planId, string contractTime) {

            var invoice = await invoiceService.GetInvoice(userId);

            if(invoice is null) {
                await invoiceService.CreateInvoice(userId, planId, contractTime);
                return Ok("Faturalı Hattınız Tanımlandı.");
            }
            else {
                await invoiceService.AddInvoice(invoice, planId, contractTime);
                return Ok("Faturalı Hattınız Değiştirildi.");
            }
        }
    }
}
