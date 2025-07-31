using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Extensions;
using TalabatProject.Core.Entity.Identity;
using TalabatProject.Core.Entity.Order_Aggregatoin;
using TalabatProject.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(UserManager<AppUser> userManager,IOrderService orderService,IMapper mapper)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("create_order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto model)
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            var address = mapper.Map<AddressDto, TalabatProject.Core.Entity.Order_Aggregatoin.Address>(model.Address);

            var basketId = model.BasketId;
            var deliveryMethodId = model.DeliveryMethodId;

            var order = await orderService.CreateOrderAsync(user.Email, basketId, deliveryMethodId, address);

            if (order is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(order);
        }
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdForUserAsync(email, id);
            if (order is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(order);
        }
        [HttpGet("get_orders_for_user")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetOrdersForUserAsync(email);
            return Ok(orders);
        }
        [HttpGet("get_DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
        {
            var methods = await orderService.GetAllDeliveryMethods();
            return Ok(methods);
        }
    }
}
