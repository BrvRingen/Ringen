using System;
using System.Net;
using Http.Library.Models;
using Http.Library.Services;
using Ringen.Schnittstelle.RDB.Models;
using Ringen.Schnittstelle.RDB.Services;

namespace Ringen.Schnittstelle.RDB.Factories
{
    internal class RdbServiceErsteller
    {
        private const string Schnittstelle = "RDB";

        private static RdbSystemSettings _settings { get; set; } = null;

        public static void Init(RdbSystemSettings systemSettings)
        {
            if (systemSettings == null && _settings == null)
            {
                throw new ArgumentException($"{Schnittstelle} System-Settings nicht definiert. Bitte übergeben Sie System-Settings.");
            }
            else
            {
                _settings = systemSettings;
            }
        }

        public static RdbService ErstelleService()
        {
            if (_settings == null)
            {
                throw new ArgumentException($"{Schnittstelle} System-Settings nicht definiert. Bitte rufen Sie zuerst die Init(...)-Methode auf.");
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

            HttpServiceSettings httpServiceSettings = new HttpServiceSettings(_settings.Credentials)
            {
                Authorization = RequestAuthorization.Basic
            };
            HttpService httpService = new HttpService($"{Schnittstelle}", httpServiceSettings);

            RdbService service = new RdbService(httpService, _settings);

            return service;
        }

        public static RdbSystemSettings GetCurrentSystemSettings()
        {
            return _settings;
        }
    }
}
