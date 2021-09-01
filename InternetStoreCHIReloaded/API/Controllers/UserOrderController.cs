using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserOrderController : JwtFeaturedController
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public UserOrderController(
            IOrdersService ordersService,
            IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<GenericResponse> MakeAnOrder()
        {
            int userId = GetUserId();
            var result = await _ordersService.MakeAnOrderAsync(userId);
            var response = _mapper.Map<GenericResponse>(result);
            return response;
        }
    }
}
