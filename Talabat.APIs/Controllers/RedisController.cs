using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class RedisController : ApiBaseController
    {
        private readonly IBasketRepository basketRepo;
        private readonly IMapper mapper;

        public RedisController(IBasketRepository basketRepo, IMapper mapper)
        {
            this.basketRepo = basketRepo;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await basketRepo.GetBasketAsync(id);
            if (basket is null) return BadRequest(new ApiErrorResponse(400));
           return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var cust = await basketRepo.UpdateBasketAsync(mappedBasket);
            return cust is null ? new CustomerBasketDto(basket.Id) : Ok(cust);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await basketRepo.DeleteBasketAsync(id);
        }
    }
}
