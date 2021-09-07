using AutoMapper;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests
{
    public class OrdersServiceTests
    {
        private readonly IMapper _mapper;

        public OrdersServiceTests(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public async Task MakeAnOrder_ValidDataUserExistsAndHasCartItems_ExpectedSuccessTrue()
        {
            var timeNow = DateTime.UtcNow;
            var products = new List<ProductEntity>()
            {
                new ProductEntity
                {
                    Id = 1,
                    CategoryId = 1,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow,
                    Description = "desc1",
                    Name = "Name1",
                    Price = 123
                },
                new ProductEntity
                {
                    Id = 2,
                    CategoryId = 1,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow,
                    Description = "desc2",
                    Name = "Name2",
                    Price = 456
                }
            };

            var cartItems = new List<CartItemEntity>()
            {
                new CartItemEntity
                {
                    Id = 1,
                    CartId = 1,
                    ProductId = 1,
                    Product = products[0],
                    Quantity = 3
                },
                new CartItemEntity
                {
                    Id = 2,
                    CartId = 1,
                    ProductId = 2,
                    Product = products[1],
                    Quantity = 5
                }
            };

            var carts = new List<CartEntity>
            {
                new CartEntity
                {
                    Id = 1,
                    UserId = 1,
                    CartItems = cartItems
                }
            };

            var cartsRepositoryMock = new Mock<ICartsRepository>();
            cartsRepositoryMock
                .Setup(cr => cr.IsUserCartEmptyAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) =>
                {
                    var cart = carts.Single(c => c.UserId == userId);
                    return !cart.CartItems.Any();
                });
            cartsRepositoryMock
                .Setup(cr => cr.GetAllItemsInUserCartAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) => carts.Single(c => c.UserId == userId).CartItems.ToList());
            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock
                .Setup(or => or.MakeAnOrder(It.IsAny<List<CartItemEntity>>(), It.IsAny<OrderEntity>()))
                .ReturnsAsync((List<CartItemEntity> cartItems, OrderEntity order) =>
                {
                    if (cartItems != null && cartItems.Any() && order != null)
                        return new DbResponse
                        {
                            IsSuccessful = true
                        };
                    else
                        return new DbResponse
                        {
                            IsSuccessful = false,
                            Message = "Error"
                        };
                });

            var ordersService = new OrdersService(
                _mapper,
                cartsRepositoryMock.Object,
                ordersRepositoryMock.Object);

            var actual = await ordersService.MakeAnOrderAsync(1);

            Assert.True(actual.IsSuccessful);
            Assert.Equal("Order was completed!", actual.Message);
        }

        [Fact]
        public async Task MakeAnOrder_ValidDataUserExistsNoCartItems_ExpectedSuccessFalse()
        {
            var products = new List<ProductEntity>();

            var cartItems = new List<CartItemEntity>();

            var carts = new List<CartEntity>
            {
                new CartEntity
                {
                    Id = 1,
                    UserId = 1,
                    CartItems = cartItems
                }
            };

            var cartsRepositoryMock = new Mock<ICartsRepository>();
            cartsRepositoryMock
                .Setup(cr => cr.IsUserCartEmptyAsync(It.IsAny<int>()))
                .ReturnsAsync((int userId) =>
                {
                    var cart = carts.Single(c => c.UserId == userId);
                    return !cart.CartItems.Any();
                });

            var ordersService = new OrdersService(
                _mapper,
                cartsRepositoryMock.Object,
                null);

            var actual = await ordersService.MakeAnOrderAsync(1);

            Assert.False(actual.IsSuccessful);
            Assert.Equal("Cart is empty.", actual.Message);
        }
    }
}
