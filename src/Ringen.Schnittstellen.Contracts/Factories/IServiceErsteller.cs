using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Schnittstellen.Contracts.Factories
{
    public interface IServiceErsteller
    {
        T GetService<T>();
    }
}
