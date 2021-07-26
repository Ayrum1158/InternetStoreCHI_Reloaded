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
        public IEnumerable<string> Get()// return all
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)// return specific
        {
            return "value";
        }

        [HttpPost]
        public string Post([FromBody] CategoryVM newCategory)// POST aka Create
        {
            var response = categoryService.AddCategory(new CategoryContract()
            {
                CategoryName = newCategory.CategoryName,
                CategoryDescription = newCategory.CategoryDescription
            });
            return response.Message;
        }

        [HttpPut]// PUT aka Update
        public string Put([FromBody] CategoryVM updatedCategory)
        {
            var response = categoryService.UpdateCategory(new CategoryContract
            {
                Id = updatedCategory.CategoryId,
                CategoryName = updatedCategory.CategoryName,
                CategoryDescription = updatedCategory.CategoryDescription
            });

            return response.Message;
        }

        [HttpDelete]
        public void Delete(string categoryName)
        {
        }
    }
}
