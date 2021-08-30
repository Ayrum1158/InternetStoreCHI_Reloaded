using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartsRepository _cartsRepository;
        private readonly IMapper _mapper;

        public OrdersService(
            IMapper mapper,
            ICartsRepository cartsRepository,
            IOrdersRepository ordersRepository)
        {
            _mapper = mapper;
            _cartsRepository = cartsRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<ServiceResult> MakeAnOrder(int userId)// no user validation because we retrieve userId via JWT
        {
            bool isCartEmpty = await _cartsRepository.IsUserCartEmptyAsync(userId);
            if (isCartEmpty)// if no items in cart
            {
                return new ServiceResult()
                {
                    IsSuccessful = false,
                    Message = "Cart is empty."
                };
            }

            var cartItemEntities = await _cartsRepository.GetAllItemsInUserCartAsync(userId);
            var cartItems = _mapper.Map<List<CartItem>>(cartItemEntities);
            DateTime orderCreationdate = DateTime.UtcNow;
            List<OrderedItem> orderedItems = cartItems.Select(ci => new OrderedItem()
            {
                Date = orderCreationdate,
                Price = ci.Product.Price,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            }).ToList();
            Order order = new Order()
            {
                CreationDate = orderCreationdate,
                OrderedItems = orderedItems,
                TotalSum = orderedItems.Sum(op => op.Price),
                UserId = userId
            };
            OrderEntity orderEntity = _mapper.Map<OrderEntity>(order);
            var dbResponse = await _ordersRepository.MakeAnOrder(cartItemEntities, orderEntity);
            var result = _mapper.Map<ServiceResult>(dbResponse);
            if(result.IsSuccessful)
            {
                result.Message = "Order was completed!";
            }
            else
            {
                result.Message = "An error occured while processing order.";
            }
            return result;
        }
    }
}
