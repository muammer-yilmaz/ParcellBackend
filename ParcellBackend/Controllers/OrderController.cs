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
    public class OrderController : ControllerBase {
        private readonly OrderServiceRepository orderService;

        public OrderController(OrderServiceRepository orderService) {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<List<Order>> GetOrders() {
            var orderList = await orderService.GetList();

            return orderList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> GetOrder(string id) {
            var order = await orderService.Get(id);


            if (order is null) {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order newOrder) {

            await orderService.Create(newOrder);

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateOrder(string id, Order updatedOrder) {
            var order = await orderService.Get(id);

            if (order is null) {
                return NotFound();
            }

            updatedOrder.Id = order.Id;

            await orderService.Update(id, updatedOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteOrder(string id) {
            var order = await orderService.Get(id);

            if (order is null) {
                return NotFound();
            }

            await orderService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(string userId ,OrderItem orderItem) {

            await orderService.AddOrder(userId,orderItem);

            return Ok(orderItem);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderItem>>> GetUserOrders(string userId) {
            var list = await orderService.GetUserOrders(userId);

            if(list is null || list.Count == 0) {
                return NotFound("Önceden Verilen Sipariş Bulunamadı");
            }
            return Ok(list);
        }
     }

}

