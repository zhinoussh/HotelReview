using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace HotelAdvice.DataAccessLayer
{
    public interface ICacheProvider
    {
        IEnumerable<T> GetOrSet<T>(string key, Func<IEnumerable<T>> methodToCall, TimeSpan duration
                                   ,object cacheDependency) where T : class;
        object GetCacheItem(string key);
        void SetCacheItem(string key, object data, TimeSpan duration, object cacheDependency);
        void InvalidateCache(string dependencyArray);
    }

    public class CacheProvider : ICacheProvider
    {
        private static readonly object CacheLockObject = new object();

        public object GetCacheItem(string key)
        {
            return HttpRuntime.Cache[key];   
        }

        public void SetCacheItem(string key, object data, TimeSpan duration, object cacheDependency)
        {
            CacheDependency dependency=new CacheDependency(null,(string[])cacheDependency);
            HttpRuntime.Cache.Insert(key,data,dependency, DateTime.Now.Add(duration),Cache.NoSlidingExpiration);
        }


        public IEnumerable<T> GetOrSet<T>(string key, Func<IEnumerable<T>> methodToCall, TimeSpan duration, object cacheDependency) where T : class
        {
            IEnumerable<T> result = GetCacheItem(key) as IEnumerable<T>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = methodToCall();
                    SetCacheItem(key, result, duration, cacheDependency);
                }
            }
            return result;
        }

        public void InvalidateCache(string dependencyArray)
        {
            HttpRuntime.Cache.Remove(dependencyArray[0]);
        }
    }
}