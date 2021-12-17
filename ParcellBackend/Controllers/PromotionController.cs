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
    public class PromotionController : ControllerBase
    {
        private readonly PromotionServiceRepository promotionService;
        public PromotionController(PromotionServiceRepository promotionService)
        {
            this.promotionService = promotionService;
        }


        [HttpGet]
        public async Task<List<Promotion>> GetPromotions()
        {
            var promotionList = await promotionService.GetList();

            return promotionList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Promotion>> GetPromotion(string id)
        {
            var promotion = await promotionService.Get(id);


            if (promotion is null)
            {
                return NotFound();
            }

            return promotion;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion(Promotion newPromotion)
        {

            await promotionService.Create(newPromotion);

            return CreatedAtAction(nameof(GetPromotion), new { id = newPromotion.Id }, newPromotion);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdatePromotion(string id, Promotion updatedPromotion)
        {
            var promotion = await promotionService.Get(id);

            if (promotion is null)
            {
                return NotFound();
            }

            updatedPromotion.Id = promotion.Id;

            await promotionService.Update(id, updatedPromotion);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeletePromotion(string id)
        {
            var promotion = await promotionService.Get(id);

            if (promotion is null)
            {
                return NotFound();
            }

            await promotionService.Delete(id);

            return NoContent();
        }

    }
}
