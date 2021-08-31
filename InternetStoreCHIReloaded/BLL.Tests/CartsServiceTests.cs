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

        public CartsServiceTests(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public async Task AddToUserCartAsync_InputedValidDataAddCalled_ExpectedSuccessTrue()
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
            //GetAmountOfItemsInUserCartAsync
            cartsRepositoryMock
                .Setup(cr => cr.GetAmountOfItemsInUserCartAsync(It.IsAny<int>()))
                .ReturnsAsync((int cartId) =>
                {
                    if (cartId == cartEntity.Id)
                        return userCartItems.Count;
                    else
                        throw new ArgumentException("No cart found");
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

        [Fact]
        public async Task AddToUserCartAsync_InputedValidDataUpdateCalled_ExpectedSuccessTrue()
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
                    Description = "Product1Desc",
                    Name = "Product1",
                    Price = 123
                }
            };
            var userCartItems = new List<CartItemEntity>()
            {
                new CartItemEntity
                {
                    CartId = 1,
                    Id = 1,
                    Product = products[0],
                    ProductId = products[0].Id,
                    Quantity = 2
                }
            };
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
            //GetAmountOfItemsInUserCartAsync
            cartsRepositoryMock
                .Setup(cr => cr.GetAmountOfItemsInUserCartAsync(It.IsAny<int>()))
                .ReturnsAsync((int cartId) =>
                {
                    if (cartId == cartEntity.Id)
                        return userCartItems.Count;
                    else
                        throw new ArgumentException("No cart found");
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
                    ProductId = 1,
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

        [Fact]
        public async Task AddToUserCartAsync_InputedInvalidDataLowQuantity_ExpectedSucessFalse()
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
                }
            };

            var mapperMock = new Mock<IMapper>();

            var cartsConfigMock = new Mock<IOptionsMonitor<CartsConfig>>();
            cartsConfigMock.Setup(cc => cc.CurrentValue).Returns(new CartsConfig());

            var addToCartModel = new AddToCartModel
            {
                ProductId = 1,
                Quantity = 0
            };

            var cartsService = new CartsService(
                mapperMock.Object,
                null,
                null,
                cartsConfigMock.Object);

            // act:

            var actual = await cartsService.AddToUserCartAsync(1, addToCartModel);

            // assert:

            Assert.False(actual.IsSuccessful);
            Assert.Equal("Quantity is less then 1.", actual.Message);
        }

        [Fact]
        public async Task AddToUserCartAsync_InputedInvalidDataNonExistingProductId_ExpectedSucessFalse()
        {
            // arrange:

            var products = new List<ProductEntity>();

            var mapperMock = new Mock<IMapper>();
            var productGenericRepositoryMock = new Mock<IGenericRepository<ProductEntity>>();
            //IsPresentInDbAsync
            productGenericRepositoryMock
                .Setup(pgr => pgr.IsPresentInDbAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
                .ReturnsAsync((Expression<Func<ProductEntity, bool>> expr) => products.Any(expr.Compile()));
            var cartsConfigMock = new Mock<IOptionsMonitor<CartsConfig>>();
            cartsConfigMock.Setup(cc => cc.CurrentValue).Returns(new CartsConfig());

            var addToCartModel = new AddToCartModel
            {
                ProductId = 0,
                Quantity = 5
            };

            var cartsService = new CartsService(
                mapperMock.Object,
                productGenericRepositoryMock.Object,
                null,
                cartsConfigMock.Object);

            // act:

            var actual = await cartsService.AddToUserCartAsync(1, addToCartModel);

            // assert:

            Assert.False(actual.IsSuccessful);
            Assert.Equal("No product found.", actual.Message);
        }

        [Fact]
        public async Task AddToUserCartAsync_InputedValidDataExceededMaximumItemQuantity_ExpectedSuccessFalse()
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
                    Description = "Product1Desc",
                    Name = "Product1",
                    Price = 123
                }
            };

            var userCartItems = new List<CartItemEntity>()
            {
                new CartItemEntity
                {
                    CartId = 1,
                    Id = 1,
                    Product = products[0],
                    ProductId = products[0].Id,
                    Quantity = 9
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
            var cartsConfigMock = new Mock<IOptionsMonitor<CartsConfig>>();
            cartsConfigMock.Setup(cc => cc.CurrentValue).Returns(cartsConfig);

            var cartsService = new CartsService(
                _mapper,
                productGenericRepositoryMock.Object,
                cartsRepositoryMock.Object,
                cartsConfigMock.Object);

            var addToCartModel = new AddToCartModel
            {
                ProductId = 1,
                Quantity = 2
            };

            // act:

            var actual = await cartsService.AddToUserCartAsync(1, addToCartModel);

            // assert:

            Assert.False(actual.IsSuccessful);
            Assert.Equal($"Quantity of one individual item cannot exceed {cartsConfig.MaximumItemQuantity}.", actual.Message);
        }

        [Fact]
        public async Task AddToUserCartAsync_InputedValidDataExceededMaximumUniqueItemsInCart_ExpectedSuccessFalse()
        {
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

            var userCartItems = new List<CartItemEntity>()
            {
                new CartItemEntity
                {
                    CartId = 1,
                    Id = 1,
                    Product = products[0],
                    ProductId = products[0].Id,
                    Quantity = 2
                }
            };

            var cartsConfig = new CartsConfig
            {
                MaximumItemQuantity = 10,
                MaximumItemsInCart = 1
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
            var cartsConfigMock = new Mock<IOptionsMonitor<CartsConfig>>();
            cartsConfigMock.Setup(cc => cc.CurrentValue).Returns(cartsConfig);
            //GetAmountOfItemsInUserCartAsync
            cartsRepositoryMock
                .Setup(cr => cr.GetAmountOfItemsInUserCartAsync(It.IsAny<int>()))
                .ReturnsAsync((int cartId) =>
                {
                    if (cartId == cartEntity.Id)
                        return userCartItems.Count;
                    else
                        throw new ArgumentException("No cart found");
                });

            var cartsService = new CartsService(
                _mapper,
                productGenericRepositoryMock.Object,
                cartsRepositoryMock.Object,
                cartsConfigMock.Object);

            var addToCartModel = new AddToCartModel
            {
                ProductId = 1,
                Quantity = 2
            };

            // act:

            var actual = await cartsService.AddToUserCartAsync(1, addToCartModel);

            // assert:

            Assert.False(actual.IsSuccessful);
            Assert.Equal($"Amount of individual items in cart cannot exceed {cartsConfig.MaximumItemsInCart}.", actual.Message);
        }
    }
}
