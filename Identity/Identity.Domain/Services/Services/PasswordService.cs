using Module.Identity.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Module.Identity.Domain.Services.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher =
        new PasswordHasher<object>();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool Verify(string password, string passwordHash)
        {
          var pass =  _hasher.HashPassword(null,password);
            var result = _hasher.VerifyHashedPassword(
                null,
                passwordHash,
                password);

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
