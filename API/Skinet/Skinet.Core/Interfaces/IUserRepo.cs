using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Skinet.Core.Entities;

namespace Skinet.Core.Interfaces
{
   public interface IUserRepo
   {
      Task<UserEntity> RegisterUserAsync(UserEntity user);
      Task<UserEntity> LogInUserAsync(UserEntity user);
      Task<string> GenerateTokenAsync(string key, string issuer, UserEntity user, int expiryMins = 10);
      Task<bool> ExistUserAsync(string userEmail);
   }
}
