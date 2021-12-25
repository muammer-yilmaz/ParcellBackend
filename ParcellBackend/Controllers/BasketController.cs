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
    public class BasketController : ControllerBase {
        private readonly BasketServiceRepository basketService;

        public BasketController(BasketServiceRepository basketService) {
            this.basketService = basketService;
        }

        [HttpGet]
        public async Task<List<Basket>> GetBaskets() {
            var textList = await basketService.GetList();

            return textList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Basket>> GetBasket(string id) {
            var text = await basketService.Get(id);


            if (text is null) {
                return NotFound();
            }

            return text;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(Basket newBasket) {

            await basketService.Create(newBasket);

            return CreatedAtAction(nameof(GetBasket), new { id = newBasket.Id }, newBasket);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateBasket(string id, Basket updatedBasket) {
            var text = await basketService.Get(id);

            if (text is null) {
                return NotFound();
            }

            updatedBasket.Id = text.Id;

            await basketService.Update(id, updatedBasket);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteBasket(string id) {
            var text = await basketService.Get(id);

            if (text is null) {
                return NotFound();
            }

            await basketService.Delete(id);

            return NoContent();
        }
        //[HttpGet]
        //public async Task<ActionResult> AddPlanToBasket(string userId, string planId) {

        //    await basketService.AddPlanToBasket(userId, planId);

        //    return Ok();
        //}

        [HttpGet]
        public async Task<ActionResult<Basket>> GetUserBasket(string userId) {
            var basket = await basketService.GetUserBasket(userId);

            if (basket is null) {
                return NotFound();
            }
            else {
                return basket;
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> CheckPlan(string userId) {

        //    var response = await basketService.CheckPlan(userId);

        //    if(response is null) {
        //        return Ok("Sepette Paket Bulunmuyor.");
        //    }
        //    else {
        //        return NotFound("Sepetinizde Bir Paket Bulunuyor. Değiştirmek İster misiniz?");
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult> AddDeviceToBasket(string userId, string deviceId) {

            var response = await basketService.CheckBasketDevices(userId);

            if(response.Count == 5) {
                return BadRequest("Sepetinizde Maksimum 5 Cihaz Bulunabilir.");
            }

            await basketService.AddDeviceToBasket(userId, deviceId);

            return Ok();
        }
        //[HttpDelete]
        //public async Task<ActionResult> DeleteBasketPlan(string userId) {
        //    await basketService.DeleteBasketPlan(userId);
        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<ActionResult> DeleteBasketDevice(string userId, string deviceId) {
            await basketService.DeleteBasketDevice(userId, deviceId);
            return NoContent();
        }
    }
}
