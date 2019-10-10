using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<string> CreateUserAsync(string userName, string password);
    }
}
