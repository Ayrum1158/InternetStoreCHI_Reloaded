using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : JwtFeaturedController
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(
            IUsersService usersService,
            IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<GenericResponse> Register(UserRegistrationViewModel newUserViewModel)
        {
            var newUserModel = _mapper.Map<UserRegistrationModel>(newUserViewModel);

            var result = await _usersService.RegisterUserAsync(newUserModel);

            var response = _mapper.Map<GenericResponse>(result);
            return response;
        }

        [HttpPost]
        public async Task<GenericResponse<string>> Login(LoginViewModel loginViewModel)
        {
            // no model state verification needed, LoginViewModel has data annotations that do this work
            var loginModel = _mapper.Map<UserLoggingInModel>(loginViewModel);
            var result = await _usersService.LoginUserAsync(loginModel);
            var response = _mapper.Map<GenericResponse<string>>(result);
            return response;
        }
    }
}
