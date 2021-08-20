using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
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
    public class UserCartController : JwtFeaturedController
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserCartController(
            IUsersService usersService,
            IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<GenericResponse> AddToCart(AddToCartViewModel atcViewModel)
        {
            int userId = GetUserId();

            var atcModel = _mapper.Map<AddToCartModel>(atcViewModel);

            var result = await _usersService.AddToUserCart(userId, atcModel);

            var response = _mapper.Map<GenericResponse>(result);

            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<GenericResponse> RemoveFromCart(RemoveFromCartViewModel rfcViewModel)
        {
            int userId = GetUserId();

            var rfcModel = _mapper.Map<RemoveFromCartModel>(rfcViewModel);

            var result = await _usersService.RemoveFromUserCart(userId, rfcModel);

            var response = _mapper.Map<GenericResponse>(result);

            return response;
        }
    }
}
