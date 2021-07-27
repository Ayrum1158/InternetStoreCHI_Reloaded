using API.ViewModels;
using AutoMapper;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)// ctor
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ResultContract<IEnumerable<CategoryVM>> Get()// return all
        {
            var result = categoryService.GetCategories();

            var response = new ResultContract<IEnumerable<CategoryVM>>() { IsSuccessful = result.IsSuccessful, Message = result.Message };

            if (result.IsSuccessful == true)
            {
                var data = mapper.Map<List<CategoryVM>>(result.Data);
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
                response.Data = mapper.Map<CategoryVM>(category);
            }

            return response;
        }

        [HttpPost]
        public string Post([FromBody] CategoryVM newCategory)// POST aka Create
        {
            var contract = mapper.Map<CategoryContract>(newCategory);
            var result = categoryService.AddCategory(contract);
            return result.Message;
        }

        [HttpPut]// PUT aka Update
        public ResultContract Put([FromBody] CategoryVM updatedCategory)
        {
            var categoryContract = mapper.Map<CategoryContract>(updatedCategory);
            var result = categoryService.UpdateCategory(categoryContract);
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
