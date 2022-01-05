using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Helper;

namespace Skinet.Infrastructure.Data
{
    public class UserRepo:IUserRepo
    {
        private readonly StoreContext _context;

        public UserRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<UserEntity> RegisterUserAsync(UserEntity user)
        {
            if (! await _context.UserEntities.AnyAsync(x => x.UserEmail == user.UserEmail))
            {
                user.UserPassword = Md5Helper.Encode(user.UserPassword);
                _context.UserEntities.Add(user);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return user;
                }

                return null;
            }

            return null;
        }

        public async Task<UserEntity> LogInUserAsync(UserEntity user)
        {
            if (await _context.UserEntities.AnyAsync(x => x.UserEmail == user.UserEmail))
            {
                var userEntity = await _context.UserEntities.FirstOrDefaultAsync(x => x.UserEmail == user.UserEmail);
                if (userEntity.UserPassword == Md5Helper.Encode(user.UserPassword))
                {
                    return userEntity;
                }
            }

            return null;
        }

        public Task<string> GenerateTokenAsync(string key, string issuer, UserEntity user,int expiryMins=10)
        {
       
            var claims = new[] {
                new Claim("Username", user.UserName),
                new Claim("UserEmail", user.UserEmail)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(expiryMins), signingCredentials: credentials);
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenDescriptor));
        }

        public async Task<bool> ExistUserAsync(string userEmail)
        {
            return await _context.UserEntities.AnyAsync(x => x.UserEmail == userEmail);
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = mySecurityKey,
                    }, out SecurityToken validatedToken);
            }
            catch {
                return false;
            }
            return true;
        }
    }
}
