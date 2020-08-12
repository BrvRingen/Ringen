using System;
using System.Security;
using System.Threading.Tasks;

namespace Ringen.Core.Services
{
    public interface ILoginService
    {
        string UserName { get; set; }
        SecureString Password { get; set; }
    }
}