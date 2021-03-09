using System;
using System.Collections.Generic;
using System.Text;

namespace WebService.Core.Interfaces.ServicesInterfaces
{
    public interface IPasswordService
    {
        bool Check(string hash, string password);
        string Hash(string password);
    }
}
