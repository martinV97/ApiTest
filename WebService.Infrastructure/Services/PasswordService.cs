using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;
using WebService.Core.CustomEntities;
using WebService.Core.Interfaces.ServicesInterfaces;

namespace WebService.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format");
            
            using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(parts[1]), Convert.ToInt32(parts[0])))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(Convert.FromBase64String(parts[2]));
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
