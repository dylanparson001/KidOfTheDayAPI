﻿using KidOfTheDayAPI.Controllers;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KidOfTheDayAPI.Dtos;
using BC = BCrypt.Net.BCrypt;


namespace KidOfTheDayAPI.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthController(
            IConfiguration config,
            IUserRepository userRepository
        )
        {
            _config = config;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto login)
        {
            User user = await AuthenticateUser(login);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJSONWebToken(user);
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Role = user.Role,
                Token = token
            };
            return Ok(userDto);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if (register == null)
            {
                return BadRequest();
            }

            register.Username = register.Username.ToUpper();

            var passwordHash = BC.EnhancedHashPassword(register.Password, 13);

            await _userRepository.RegisterUser(register.Username, passwordHash, register.EmailAddress,
                register.FirstName, register.LastName, register.Role);

            return Ok();
        }

        private async Task<User> AuthenticateUser(LoginDto login)
        {
            if (login == null)
            {
                return null;
            }

            login.Username = login.Username.ToUpper();

            var password = login.Password;

            if (string.IsNullOrEmpty((password)))
            {
                return null;
            }

            User user = await _userRepository.GetUserByUsername(login.Username);

            if (BC.EnhancedVerify(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        private object GenerateJSONWebToken(User userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
                _config["Jwt:ValidAudience"],
                claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}