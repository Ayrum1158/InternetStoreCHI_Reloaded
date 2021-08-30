using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BLL.Tests
{
    public class CartsServiceTests
    {
        private readonly IUsersService _usersService;
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;

        public CartsServiceTests(
            IUsersService usersService,
            ICategoriesService categoriesService,
            IProductsService productsService,
            ICartsService cartsService)
        {
            _usersService = usersService;
            _categoriesService = categoriesService;
            _productsService = productsService;
            _cartsService = cartsService;
        }

        [Fact]
        public void ItemsAreAddingToCartSuccessfully()
        {
            // prep:
            var timeNow = DateTime.UtcNow;
            var category = new Category
            {
                CategoryId = 1,
                CategoryDescription = "asdfaasdf",
                CategoryName = "Category1",
                CreatedDate = timeNow,
                UpdatedDate = timeNow
            };
            _categoriesService.AddCategoryAsync(category);

            var products = new Product[]
            {
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow,
                    Description = "Product1Desc",
                    Name = "Product1",
                    Price = 123
                },
                new Product
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
            foreach(var p in products)
            {
                _productsService.AddProductAsync(p);
            }

            var userRegistrationModel = new UserRegistrationModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158",
                ConfirmPassword = "SaSha-K1158",
                FirstName = "Ole",
                LastName = "Koro"
            };
            _usersService.RegisterUserAsync(userRegistrationModel);

            // act:

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
            foreach(var model in addToCartModels)
            {
                success = success && _cartsService.AddToUserCart(1, model).Result.IsSuccessful;// first user in db is supposed to have Id: 1
            }

            Assert.True(success);
        }
    }
}
