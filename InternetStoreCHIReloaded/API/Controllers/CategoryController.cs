using API.ViewModels;
using BLL.Contracts;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)// ctor
        {
            this.categoryService = categoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ResultContract<IEnumerable<CategoryVM>> Get()// return all
        {
            var result = categoryService.GetCategories();

            var response = new ResultContract<IEnumerable<CategoryVM>>() { IsSuccessful = result.IsSuccessful, Message = result.Message };

            if (result.IsSuccessful == true)
            {
                var data = result.Data.Select(c =>
                    new CategoryVM()
                    {
                        CategoryId = c.Id,
                        CategoryName = c.CategoryName,
                        CategoryDescription = c.CategoryDescription,
                        CreatedDate = c.CreatedDate,
                        UpdatedDate = c.UpdatedDate
                    });

                response.Data = data;
            }

            return response;
        }

        [HttpGet("{id}")]
        public ResultContract<CategoryVM> Get(int id)// return specific
        {
            var result = categoryService.GetCategory(id);

            var response = new ResultContract<CategoryVM>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

            if (result.IsSuccessful == true)
            {
                var category = result.Data;
                response.Data = new CategoryVM()
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    CreatedDate = category.CreatedDate,
                    UpdatedDate = category.UpdatedDate
                };
            }

            return response;
        }

        [HttpPost]
        public string Post([FromBody] CategoryVM newCategory)// POST aka Create
        {
            var result = categoryService.AddCategory(new CategoryContract()
            {
                CategoryName = newCategory.CategoryName,
                CategoryDescription = newCategory.CategoryDescription
            });
            return result.Message;
        }

        [HttpPut]// PUT aka Update
        public ResultContract Put([FromBody] CategoryVM updatedCategory)
        {
            var result = categoryService.UpdateCategory(new CategoryContract
            {
                Id = updatedCategory.CategoryId,
                CategoryName = updatedCategory.CategoryName,
                CategoryDescription = updatedCategory.CategoryDescription
            });

            return result;
        }

        [HttpDelete("{id}")]
        public ResultContract Delete(int id)
        {
            var result = categoryService.DeleteCategory(id);
            return result;
        }
    }
}
