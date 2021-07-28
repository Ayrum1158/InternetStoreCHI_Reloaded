﻿using API.ViewModels;
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
        public ResultContract<IEnumerable<CategoryViewModel>> Get()// return all
        {
            var result = categoryService.GetCategories();

            var response = new ResultContract<IEnumerable<CategoryViewModel>>() { IsSuccessful = result.IsSuccessful, Message = result.Message };

            if (result.IsSuccessful == true)
            {
                var data = mapper.Map<List<CategoryViewModel>>(result.Data);
                response.Data = data;
            }

            return response;
        }

        [HttpGet("{id}")]
        public ResultContract<CategoryViewModel> Get(int id)// return specific
        {
            var result = categoryService.GetCategory(id);

            var response = new ResultContract<CategoryViewModel>()
            {
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

            if (result.IsSuccessful == true)
            {
                var category = result.Data;
                response.Data = mapper.Map<CategoryViewModel>(category);
            }

            return response;
        }

        [HttpPost]
        public string Post([FromBody] CategoryViewModel newCategory)// POST aka Create
        {
            var contract = mapper.Map<Category>(newCategory);
            var result = categoryService.AddCategory(contract);
            return result.Message;
        }

        [HttpPut]// PUT aka Update
        public ResultContract Put([FromBody] CategoryViewModel updatedCategory)
        {
            var categoryContract = mapper.Map<Category>(updatedCategory);
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
