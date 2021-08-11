using API.ViewModels;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(
            IUsersService usersService,
            IMapper mapper
            )
        {
            _usersService = usersService;
            _mapper = mapper;;
        }

        [HttpPost]
        public async Task<GenericResponse> Register(UserRegistrationViewModel newUserVM)
        {
            var newUserModel = _mapper.Map<UserRegistrationModel>(newUserVM);

            var result = await _usersService.RegisterUserAsync(newUserModel);

            var response = _mapper.Map<GenericResponse>(result);
            return response;
        }

        [HttpPost]
        public async Task<GenericResponse<string>> Login(LoginViewModel loginVM)
        {
            if(ModelState.IsValid)
            {
                var loginModel = _mapper.Map<UserLoggingInModel>(loginVM);
                var result = await _usersService.LoginUserAsync(loginModel);
                var response = _mapper.Map<GenericResponse<string>>(result);
                return response;
            }
            else
            {
                var response = new GenericResponse<string>();
                response.IsSuccessful = false;
                response.Message = "Login data is not valid.";
                return response;
            }
        }
    }
}
