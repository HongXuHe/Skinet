using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Skinet.API.Dtos;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;

namespace Skinet.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepo _userRepo;

        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AccountController(IUserRepo userRepo, IConfiguration config, IMapper mapper)
        {
            _userRepo = userRepo;
            _config = config;
            _mapper = mapper;            
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var userEntity = await _userRepo.RegisterUserAsync(_mapper.Map<UserEntity>(user));
            if (userEntity==null)
            {
                return BadRequest();
            }
            var token = await _userRepo.GenerateTokenAsync(_config["Jwt:Key"], _config["Jwt:Issuer"], userEntity);
            var userToReturn = _mapper.Map<UserToRetrun>(userEntity);
            userToReturn.Token = token;
            return Ok(userToReturn);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(UserDto user)
        {
            var userEntity = await _userRepo.LogInUserAsync(_mapper.Map<UserEntity>(user));
            if (userEntity == null) {
                return NotFound();
            }
            var token = await _userRepo.GenerateTokenAsync(_config["Jwt:Key"], _config["Jwt:Issuer"], userEntity);
            var userToReturn = _mapper.Map<UserToRetrun>(userEntity);
            userToReturn.Token = token;
            return Ok(userToReturn);
        }

        [HttpGet("exists/{userEmail}")]
        public async Task<IActionResult> ExistsUser(string userEmail)
        {
            return Ok(await _userRepo.ExistUserAsync(userEmail));
        }

    }
}
