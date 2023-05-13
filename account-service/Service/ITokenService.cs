using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyMateAPI.Core.Model;

namespace API.Core.Service.Core
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}