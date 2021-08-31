using AutoMapper;
using BLL.ConfigPOCOs;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests
{
    public class CartsServiceTests
    {
        private readonly IMapper _mapper;
        //private readonly IUsersService _usersService;
        //private readonly ICategoriesService _categoriesService;
        //private readonly IProductsService _productsService;
        //private readonly ICartsService _cartsService;

        public CartsServiceTests(IMapper mapper)
        //IUsersService usersService,
        //ICategoriesService categoriesService,
        //IProductsService productsService,
        //ICartsService cartsService)
        {
            _mapper = mapper;
            //_usersService = usersService;
            //_categoriesService = categoriesService;
            //_productsService = productsService;
            //_cartsService = cartsService;
        }

        [Fact]
        public async Task AddToUserCartAsync_InputedValidData_ExpectedSuccess()
        {
            // arrange:

            var timeNow = DateTime.UtcNow;
            var products = new List<ProductEntity>()
            {
                new ProductEntity
                {
                    Id = 1,
                    CategoryId = 1,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow,
                    Description = "Product1Desc",
                    Name = "Product1",
                    Price = 123
                },
                new ProductEntity
                {
                    Id = 2,
                    CategoryId = 1,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow,
                    Description = "Product2Desc",
                    Name = "Product2",
                    Price = 1234
                }
            };
            var userCartItems = new List<CartItemEntity>();
            //{
            //    new CartItemEntity
            //    {
            //        CartId = 1,
            //        Id = 1,
            //        Product = products[0],
            //        ProductId = products[0].Id,
            //        Quantity = 2
            //    },
            //    new CartItemEntity
            //    {
            //        CartId = 1,
            //        Id = 2,
            //        Product = products[1],
            //        ProductId = products[1].Id,
            //        Quantity = 7
            //    },
            //};
            var cartEntity = new CartEntity
            {
                CartItems = new List<CartItemEntity>(),
                Id = 1,
                UserId = 1,
                User = new UserEntity
                {
                    Id = 1
                }
            };
            var cartsConfig = new CartsConfig
            {
                MaximumItemQuantity = 10,
                MaximumItemsInCart = 5
            };
            var productGenericRepositoryMock = new Mock<IGenericRepository<ProductEntity>>();
            //IsPresentInDbAsync
            productGenericRepositoryMock
                .Setup(pgr => pgr.IsPresentInDbAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
                .ReturnsAsync((Expression<Func<ProductEntity, bool>> expr) => products.Any(expr.Compile()));
            var cartsRepositoryMock = new Mock<ICartsRepository>();
            //GetCartIdFromUserIdAsync
            cartsRepositoryMock
                .Setup(cr => cr.GetCartIdFromUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => 1);
            //GetItemInUserCartAsync
            cartsRepositoryMock
                .Setup(cr => cr.GetItemInUserCartAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int cartId, int productId) => userCartItems.SingleOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId));
            //UpdateAsync
            cartsRepositoryMock
                .Setup(cr => cr.UpdateAsync(It.IsAny<CartItemEntity>()))
                .ReturnsAsync((CartItemEntity entity) =>
                {
                    if (userCartItems.SingleOrDefault(i => i.ProductId == entity.ProductId) == null)
                    {// no item to update
                        return false;
                    }
                    var cartItem = userCartItems.Find(i => i.ProductId == entity.ProductId);
                    userCartItems.Remove(cartItem);
                    userCartItems.Add(entity);
                    return true;
                });
            //AddAsync
            cartsRepositoryMock
                .Setup(cr => cr.AddAsync(It.IsAny<CartItemEntity>()))
                .ReturnsAsync((CartItemEntity entity) =>
                {
                    if (userCartItems.SingleOrDefault(i => i.ProductId == entity.ProductId) != null)
                    {// can't duplicate productId
                        return false;
                    }
                    userCartItems.Add(entity);
                    return true;
                });
            var cartsConfigMock = new Mock<IOptionsMonitor<CartsConfig>>();
            cartsConfigMock.Setup(cc => cc.CurrentValue).Returns(cartsConfig);
            var cartsService = new CartsService(
                _mapper,
                productGenericRepositoryMock.Object,
                cartsRepositoryMock.Object,
                cartsConfigMock.Object);
            var addToCartModels = new AddToCartModel[]
            {
                new AddToCartModel
                {
                    ProductId = 1,
                    Quantity = 3
                },
                new AddToCartModel
                {
                    ProductId = 2,
                    Quantity = 4
                },
            };
            bool success = true;

            // act:

            foreach (var model in addToCartModels)
            {
                var actual = await cartsService.AddToUserCartAsync(1, model);
                success = success && actual.IsSuccessful;
            }

            // assert

            Assert.True(success);
        }
    }
}
