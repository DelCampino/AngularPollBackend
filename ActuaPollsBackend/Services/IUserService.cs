using ActuaPollsBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        String Register(string username, string email, string password);
    }
}
