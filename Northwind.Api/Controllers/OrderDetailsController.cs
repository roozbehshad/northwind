using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Domain;
using Northwind.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase {
        private readonly IEntityService<OrderDetail> _service;

        public OrderDetailsController(IEntityService<OrderDetail> service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails() =>
            await _service.GetAllAsync();

        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int orderId, int productId) {
            var orderDetail = await _service.GetByKeyAsync(orderId, productId);

            if (orderDetail == null)
                return NotFound();

            return orderDetail;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail) {
            orderDetail.State = State.Added;
            int affectedEntries = await _service.SaveAsync(orderDetail);

            return CreatedAtAction(nameof(GetOrderDetail), new { orderId = orderDetail.OrderId, productId = orderDetail.ProductId }, orderDetail);
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> UpdateOrderDetail(int orderId, int productId, OrderDetail orderDetail) {
            if (orderId != orderDetail.OrderId || productId != orderDetail.ProductId)
                return BadRequest();

            if (!_service.Exists(orderId, productId))
                return NotFound();

            orderDetail.State = State.Modified;
            int affectedEntries = await _service.SaveAsync(orderDetail);

            return NoContent();
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId) {
            var orderDetail = await _service.GetByKeyAsync(orderId, productId);

            if (orderDetail == null)
                return NotFound();

            orderDetail.State = State.Deleted;
            int affectedEntries = await _service.SaveAsync(orderDetail);

            return NoContent();
        }
    }
}
