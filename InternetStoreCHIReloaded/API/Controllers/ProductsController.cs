﻿using API.ViewModels;
using AutoMapper;
using BLL.Models;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("GetAll")]
        public async Task<GenericResponse<IEnumerable<ProductViewModel>>> Get()
        {
            var result = await _productService.GetProductsAsync();
            var response = _mapper.Map<GenericResponse<IEnumerable<ProductViewModel>>>(result);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<GenericResponse<ProductViewModel>> Get(int id)
        {
            var result = await _productService.GetProductAsync(id);
            var response = _mapper.Map<GenericResponse<ProductViewModel>>(result);
            return response;
        }

        [HttpPost("{pageSize}/{page}")]
        public async Task<GenericResponse<List<ProductViewModel>>> Post(int pageSize, int page, ProductsFilterViewModel filterVM)
        {
            var filter = _mapper.Map<ProductsFilter>(filterVM);

            var result = await _productService.GetProductsFilteredAsync(pageSize, page, filter);

            var response = _mapper.Map<GenericResponse<List<ProductViewModel>>>(result);

            return response;
        }

        [HttpPost]
        public async Task<GenericResponse<ProductViewModel>> Post([FromBody] ProductViewModel newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            var result = await _productService.AddProductAsync(product);
            var response = _mapper.Map<GenericResponse<ProductViewModel>>(result);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<GenericResponse<ProductViewModel>> Put(int id, [FromBody] ProductViewModel updatedProduct)
        {
            if (id == updatedProduct.Id)
            {
                var product = _mapper.Map<Product>(updatedProduct);
                var result = await _productService.UpdateProductAsync(product);
                var response = _mapper.Map<GenericResponse<ProductViewModel>>(result);
                return response;
            }
            else
            {
                var response = new GenericResponse<ProductViewModel>();
                response.IsSuccessful = false;
                response.Message = "Ids in url and body do not match.";
                return response;
            }
        }

        [HttpDelete("{id}")]
        public async Task<GenericResponse> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            var response = _mapper.Map<GenericResponse>(result);
            return response;
        }
    }
}
