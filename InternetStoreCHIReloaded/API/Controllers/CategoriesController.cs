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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)// ctor
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ResultContract<IEnumerable<CategoryViewModel>> Get()// return all
        {
            var result = _categoryService.GetCategories();

            var response = new ResultContract<IEnumerable<CategoryViewModel>>() { IsSuccessful = result.IsSuccessful, Message = result.Message };

            if (result.IsSuccessful == true)
            {
                var data = _mapper.Map<List<CategoryViewModel>>(result.Data);
                response.Data = data;
            }

            return response;
        }

        [HttpGet("{id}")]
        public ResultContract<CategoryViewModel> Get(int id)// return specific
        {
            var result = _categoryService.GetCategory(id);

            var response = new ResultContract<CategoryViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

            if (result.IsSuccessful == true)
            {
                var category = result.Data;
                response.Data = _mapper.Map<CategoryViewModel>(category);
            }

            return response;
        }

        [HttpPost]
        public string Post([FromBody] CategoryViewModel newCategory)// POST aka Create
        {
            var contract = _mapper.Map<Category>(newCategory);
            var result = _categoryService.AddCategory(contract);
            return result.Message;
        }

        [HttpPut]// PUT aka Update
        public ResultContract Put([FromBody] CategoryViewModel updatedCategory)
        {
            var categoryContract = _mapper.Map<Category>(updatedCategory);
            var result = _categoryService.UpdateCategory(categoryContract);
            return result;
        }

        [HttpDelete("{id}")]
        public ResultContract Delete(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return result;
        }
    }
}
