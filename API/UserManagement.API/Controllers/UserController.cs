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
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IOptions<AuthenticationSettings> _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, IOptions<AuthenticationSettings> config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateRequest model)
        {
            var token = await _userService.AuthenticateAsync(model.Login, model.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Incorrect login or password" });
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList()
            });
            }

            var user = _mapper.Map<User>(model);

            try
            {
                await _userService.CreateAsync(user, model.Password);
                return Ok(model);
            }
            catch (UserDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<IReadOnlyCollection<UserResponse>>> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                var model = _mapper.Map<IReadOnlyCollection<UserResponse>>(users);
                return Ok(model);
            }
            catch (UserDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                var model = _mapper.Map<UserResponse>(user);
                return Ok(model);
            }
            catch (UserDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }         
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> UpdateAsync(Guid id, [FromBody]UpdateRequest model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList()
                });
            }

            var user = _mapper.Map<User>(model);
            user.SetId(id);

            try
            {
                await _userService.UpdateAsync(user, model.Password);
                return Ok(model);
            }
            catch (UserDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponse>> DeleteAsync(Guid id)
        {
            try
            {
                var user = await _userService.DeleteAsync(id);
                return Ok(user);
            }
            catch (UserDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}