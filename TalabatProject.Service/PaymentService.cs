using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using Product = TalabatProject.Core.Entity.Product;
using TalabatProject.Core.Interfaces;
using TalabatProject.Core.Repositories;
using TalabatProject.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace TalabatProject.Service
{
    [Authorize]
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration 
            configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var basket = await basketRepository.GetBasketAsync(basketId);

            if(basket == null) return null;

            foreach(var item in basket.Items)
            {
                //TO DO: get product from product repo
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            if(string.IsNullOrEmpty(basket.CustomerIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(x => x.Price * x.Quantity * 100), // total amount in cents based on basket items
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                };

                PaymentIntent paymentIntent;

                var service = new PaymentIntentService();

                paymentIntent = await service.CreateAsync(options);

                basket.CustomerIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(x => x.Price * x.Quantity * 100), // total amount in cents based on basket items
                };
            }



            await basketRepository.UpdateBasketAsync(basket);

            return basket;

        }
    }
}
