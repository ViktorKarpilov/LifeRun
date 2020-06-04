using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserManagement.API.Requests;
using UserManagement.Domain.Services;
using AutoMapper;
using UserManagement.Domain.Models;
using System.Collections.Generic;
using System;
using UserManagement.Application.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserManagement.API.Validators;
using UserManagement.API.Responses;
using System.Linq;

namespace UserManagement.API.Controllers
{
    [Route("task")]
    class TasksController:ControllerBase
    {
        private readonly IOptions<AuthenticationSettings> _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TasksController(IUserService userService, IMapper mapper, IOptions<AuthenticationSettings> config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }
        /*[AllowAnonymous]
        [HttpPost("NewTask")]
        public async Task<IActionResult> AddTask([FromBody] SetTaskRequest task)
        {
            await _userService.CreateAsync(user, model.Password);
            return Ok();
        }*/


    }
}
