using Ringen.Schnittstelle.Caching.Models;
using Ringen.Schnittstelle.Caching.Services;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching.Factories
{
    public class ServiceErstellerMitCache : IServiceErsteller
    {
        private IServiceErsteller _echterServiceErsteller;
        private CacheZeiten _cacheZeiten;

        public ServiceErstellerMitCache(IServiceErsteller echterServiceErsteller, CacheZeiten cacheZeiten)
        {
            _echterServiceErsteller = echterServiceErsteller;
            _cacheZeiten = cacheZeiten;
        }

        public T GetService<T>()
        {
            T orginalService = _echterServiceErsteller.GetService<T>();
            
            if (typeof(T) == typeof(IApiSaisonInformationen))
            {
                IApiSaisonInformationen serviceMitCache = new ApiSaisonInformationenMitCache((IApiSaisonInformationen)orginalService, _cacheZeiten);
                return (T)serviceMitCache;
            } 
            else if (typeof(T) == typeof(IApiStammdaten))
            {
                IApiStammdaten serviceMitCache = new ApiStammdatenMitCache((IApiStammdaten)orginalService, _cacheZeiten);
                return (T)serviceMitCache;
            }
            else if (typeof(T) == typeof(IApiMannschaftskaempfe))
            {
                IApiMannschaftskaempfe serviceMitCache = new ApiMannschaftskaempfeMitCache((IApiMannschaftskaempfe)orginalService, _cacheZeiten);
                return (T)serviceMitCache;
            }

            return orginalService;
        }
    }
}
