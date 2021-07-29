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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categoryService, IMapper mapper)// ctor
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GenericResponse<IEnumerable<CategoryViewModel>>> Get()// return all
        {
            var result = await _categoryService.GetCategoriesAsync();

            var response = new GenericResponse<IEnumerable<CategoryViewModel>>() { IsSuccessful = result.IsSuccessful, Message = result.Message };

            if (result.IsSuccessful == true)
            {
                var data = _mapper.Map<List<CategoryViewModel>>(result.Data);
                response.Data = data;
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<GenericResponse<CategoryViewModel>> Get(int id)// return specific
        {
            var result = await _categoryService.GetCategoryAsync(id);

            var response = _mapper.Map<GenericResponse<CategoryViewModel>>(result);

            return response;
        }

        [HttpPost]// POST aka Create
        public async Task<GenericResponse> Post([FromBody] CategoryViewModel newCategory)
        {
            var contract = _mapper.Map<Category>(newCategory);
            var result = await _categoryService.AddCategoryAsync(contract);

            var response = _mapper.Map<GenericResponse>(result);

            return response;
        }

        [HttpPut("{id}")]// PUT aka Update
        public async Task<GenericResponse<CategoryViewModel>> Put(int id, [FromBody] CategoryViewModel updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
                return new GenericResponse<CategoryViewModel>()
                {
                    IsSuccessful = false,
                    Message = "Ids in url and body are not mathing"
                };

            var categoryContract = _mapper.Map<Category>(updatedCategory);
            var result = await _categoryService.UpdateCategoryAsync(categoryContract);
            GenericResponse<CategoryViewModel> response = _mapper.Map<GenericResponse<CategoryViewModel>>(result);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<GenericResponse> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            var response = _mapper.Map<GenericResponse>(result);
            return response;
        }
    }
}
