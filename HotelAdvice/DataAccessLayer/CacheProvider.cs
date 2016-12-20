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
                                   , string[] cacheDependency) where T : class;

        T GetOrSet<T>(string key, Func<T> methodToCall, TimeSpan duration) where T : class;
        object GetCacheItem(string key);
        void SetCacheItem(string key, object data, TimeSpan duration, string[] cacheDependency);
        void InvalidateCache(string dependency);
    }

    public class CacheProvider : ICacheProvider
    {
        private static readonly object CacheLockObject = new object();

        public object GetCacheItem(string key)
        {
            return HttpRuntime.Cache[key];   
        }

        public void SetCacheItem(string key, object data, TimeSpan duration, string[] cacheDependency)
        {
            // Make sure MasterCacheKeyArray[0] is in the cache - if not, add it
            if (HttpRuntime.Cache[cacheDependency[0]] == null)
                HttpRuntime.Cache[cacheDependency[0]] = DateTime.Now;

            CacheDependency dependency=new CacheDependency(null,cacheDependency);
            HttpRuntime.Cache.Insert(key, data, dependency, DateTime.Now.Add(duration), Cache.NoSlidingExpiration);
        }


        public IEnumerable<T> GetOrSet<T>(string key, Func<IEnumerable<T>> methodToCall, TimeSpan duration
                                        , string[] cacheDependency) where T : class
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

        public T GetOrSet<T>(string key, Func<T> methodToCall, TimeSpan duration) where T : class
        {
            T result = GetCacheItem(key) as T;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = methodToCall();
                    SetCacheItem(key, result, duration, null);
                }
            }
            return result;
        }

        public void InvalidateCache(string dependency)
        {
            HttpRuntime.Cache.Remove(dependency);
        }
    }
}