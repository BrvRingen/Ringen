using System;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Plugin.Connectivity;

namespace Ringen.Schnittstellen.Caching
{
    public class ApiCache
    {
        public ApiCache()
        {
            Barrel.ApplicationId = "Bayerischer Ringer Verband e. V.";
        }

        //removes all expired data
        //Barrel.Current.EmptyExpired(); //TODO: ggf. Timer einbauen, der nach einer gewissen Zeit den gesamten Cache mal leert

        public async Task<T> Get_und_Cache_Daten<T>(string key, Func<Task<T>> apiCallMethode, TimeSpan cacheAblaufIn, bool forceRefresh = false)
        {
            if (!CrossConnectivity.Current.IsConnected) //handle online/offline scenario
            {
                return Barrel.Current.Get<T>(key: key);
            }

            if (!forceRefresh && !Barrel.Current.IsExpired(key: key))
            {
                return Barrel.Current.Get<T>(key: key);
            }
            else if (Barrel.Current.IsExpired(key: key))
            {
                //param list of keys to flush
                Barrel.Current.Empty(key: key);
            }


            T apiDaten = await apiCallMethode();

            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: key, data: apiDaten, expireIn: cacheAblaufIn);

            return apiDaten;
        }
    }
}