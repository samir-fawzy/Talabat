using Microsoft.EntityFrameworkCore.Storage;
//using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Entity.Order_Aggregatoin;
using TalabatProject.Core.Interfaces;
using TalabatProject.Core.Repositories;
using TalabatProject.Core.Services;
using TalabatProject.Repository.Specification;


namespace TalabatProject.Service
{

    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork)
        {
            this.basketRepo = basketRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Core.Entity.Order_Aggregatoin.Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            decimal subTotal = 0;
            foreach (var item in basket.Items)
                subTotal += item.Price * item.Quantity;

            var orderItems = await GetItemsInBasket(basket);

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod,orderItems, subTotal);

            await unitOfWork.Repository<Order>().AddAsync(order);

            var result = await unitOfWork.Complete();

            if (result == 0) return null;

            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethods()
        {
            return await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            return await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(new OrderSpecification(buyerEmail,orderId));
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await unitOfWork.Repository<Order>().GetAllWithSpecAsync(new OrderSpecification(buyerEmail));
        }
        private async Task<List<OrderItem>> GetItemsInBasket(CustomerBasket basket)
        {
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var productOrderItem = new ProductOrderItem()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductUrl = product.PictureUrl
                    };
                    var orderItem = new OrderItem(productOrderItem, item.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            return orderItems;
        }

    }
}
