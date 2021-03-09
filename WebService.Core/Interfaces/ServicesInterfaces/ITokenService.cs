using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces.ServicesInterfaces
{
    public interface ITokenService
    {
        Task<bool> IsValidUser(string email, string password);
    }
}
