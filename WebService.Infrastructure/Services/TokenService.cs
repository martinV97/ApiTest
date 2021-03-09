using System.Threading.Tasks;
using WebService.Core.Interfaces;
using WebService.Core.Interfaces.ServicesInterfaces;

namespace WebService.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public TokenService(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<bool> IsValidUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return _passwordService.Check(user.Password, password);
        }
    }
}
