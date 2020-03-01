using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MadPay724.Common.ErrorAndMessage;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Dtos.Site.Admin;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;
using MadPay724.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MadPay724.Presentation.Controllers.Site.Admin
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "Site")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IUnitOfWork<MadPayDbContext> dbContext, IAuthService authService, IConfiguration config)
        {
            _db = dbContext;
            _authService = authService;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExists(userForRegisterDto.UserName))
                return BadRequest(new ReturnMessage()
                {
                    status = false,
                    title = "خطا",
                    message = "نام کاربری وجود دارد"
                });

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = "",
                City = "",
                Gender = true,
                DateOfBirth = DateTime.Now,
                IsActive = true,
                Status = true
            };

            var createdUser = await _authService.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginForDto userForLoginDto)
        {
            var userFromRepo = await _authService.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (userForLoginDto == null)
                return Unauthorized("کاربری با این یوزر و کلمه عبور وجود ندارد");
                //return Unauthorized(new ReturnMessage()
                //{
                //    status = true,
                //    title = "خطا",
                //    message = "کاربری با این یوزر و پس وجود ندارد"
                //});

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = userForLoginDto.IsRemember ? DateTime.Now.AddDays(2) : DateTime.Now.AddHours(2),
                SigningCredentials = creds

            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDes);

            return Ok(new
            {
                token = tokenhandler.WriteToken(token)
            });

        }


        [AllowAnonymous]
        [HttpGet("GetValue")]
        public async Task<IActionResult> GetValue()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }

        [HttpGet("GetValues")]
        public async Task<IActionResult> GetValues()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }

    }
}