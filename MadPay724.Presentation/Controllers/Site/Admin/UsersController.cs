using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Dtos.Site.Admin.Users;
using MadPay724.Repo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadPay724.Presentation.Controllers.Site.Admin
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "Site")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUnitOfWork<MadPayDbContext> _db;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork<MadPayDbContext> dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // این متد getmany که ساختیم خیلی مهمه که با یک متد بتونیم کلی اطلاعات بیرون بکشیم
            var users = await _db.UserRepository.GetManyAsync(null, null, "Photos,BankCards");
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [Route("GetUser/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            // این شرط برای هست که اگر کاربری آیدی کاربری رو پیدا کرد نتونه اطلاعات رو بکشه بیرون
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == id)
            {
                // خیلی جالبه ببین یک مدل رو به یک مدل دیگه میرسونه 
                var user = await _db.UserRepository.GetManyAsync(p => p.Id == id, null, "Photos");
                var userToReturn = _mapper.Map<UserForDetailedDto>(user.SingleOrDefault());
                return Ok(userToReturn);
            }
            else
            {
                return Unauthorized("عدم دسترسی یا اشتباه در ورودی");
            }



        }

        
    }
}