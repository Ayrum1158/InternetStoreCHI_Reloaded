using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GenericResponse<IEnumerable<ProductViewModel>>> Get()// return all, add pagination next
        {
            var result = _productService.GetProductsAsync();
            var response = _mapper.Map<GenericResponse<IEnumerable<ProductViewModel>>>(result);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<GenericResponse<ProductViewModel>> Get(int id)// return specific
        {
            throw new NotImplementedException();
        }

        [HttpPost]// POST aka Create
        public async Task<GenericResponse> Post([FromBody] ProductViewModel newCategory)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]// PUT aka Update
        public async Task<GenericResponse<ProductViewModel>> Put(int id, [FromBody] CategoryViewModel updatedCategory)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<GenericResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
