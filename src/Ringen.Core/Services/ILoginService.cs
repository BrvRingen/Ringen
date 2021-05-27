using System.Security;

namespace Ringen.Core.Services
{
    public interface ILoginService
    {
        string UserName { get; set; }
        SecureString Password { get; set; }
    }
}