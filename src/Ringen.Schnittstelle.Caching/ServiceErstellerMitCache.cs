using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ringen.Schnittstelle.Caching.Services;
using Ringen.Schnittstellen.Contracts.Factories;
using Ringen.Schnittstellen.Contracts.Services;

namespace Ringen.Schnittstelle.Caching
{
    public class ServiceErstellerMitCache : IServiceErsteller
    {
        private IServiceErsteller _echterServiceErsteller;

        public ServiceErstellerMitCache(IServiceErsteller echterServiceErsteller)
        {
            _echterServiceErsteller = echterServiceErsteller;
        }

        public T GetService<T>()
        {
            T orginalService = _echterServiceErsteller.GetService<T>();


            if (typeof(T) == typeof(IApiSaisonInformationen))
            {
                IApiSaisonInformationen apiSaisonInformationen = (IApiSaisonInformationen)orginalService;
                IApiSaisonInformationen x = new ApiSaisonInformationenMitCache(apiSaisonInformationen);
                return (T)x;
            }

            //TODO: Weitere Services für Caching implementieren

            return orginalService;
        }
    }
}
