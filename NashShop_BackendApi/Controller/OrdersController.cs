using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
             
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = User.FindFirst("UserId")?.Value;
            request.UserId = new Guid(id);
            var order = await _orderService.Checkout(request);
            if (!order)
                return BadRequest();
            return Ok();
        }
    }
}
