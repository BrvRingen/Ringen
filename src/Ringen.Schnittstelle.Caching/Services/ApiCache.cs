using System;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Plugin.Connectivity;

namespace Ringen.Schnittstelle.Caching.Services
{
    public class ApiCache
    {
        public ApiCache()
        {
            Barrel.ApplicationId = "RingenApiCache";
        }

        public async Task<T> Get_und_Cache_Daten<T>(string key, Func<Task<T>> getDatenMethode, TimeSpan cacheAblaufIn)
        {
            //Dev handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected)
            {
                return Barrel.Current.Get<T>(key: key);
            }

            //Dev handles checking if cache is expired
            if (!Barrel.Current.IsExpired(key: key))
            {
                return Barrel.Current.Get<T>(key: key);
            }

            T apiDaten = await getDatenMethode();

            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: key, data: apiDaten, expireIn: cacheAblaufIn);

            return apiDaten;
        }
    }
}