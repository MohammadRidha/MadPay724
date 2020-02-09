using MadPay724.Common.Helpers;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;
using MadPay724.Services.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Services.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        public AuthService(IUnitOfWork<MadPayDbContext> dbContext)
        {
            _db = dbContext;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            Utilities.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.UserRepository.InserAsync(user);
            await _db.SaveAsync();

            return user;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _db.UserRepository.GetAsync(p => p.UserName == username);

            if (user == null)
            {
                return null;
            }

            if (Utilities.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

      
    }
}
