using System;
using System.Web;
using System.Collections;

namespace Infrastructure
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        public static object Get(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache.Get(CacheKey);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void Set(string CacheKey, object objObject, DateTime outTime)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, outTime, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void Remove(string CacheKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
        }
    }

    //public class CacheHelper
    //{
    //    static CachePool cache = CachePool.GetInstance();
    //    /// <summary>
    //    /// 获取数据缓存
    //    /// </summary>
    //    /// <param name="CacheKey">键</param>
    //    public static object Get(string CacheKey)
    //    {
    //        return cache.Get(CacheKey);
    //    }

    //    /// <summary>
    //    /// 设置数据缓存
    //    /// </summary>
    //    public static void Set(string CacheKey, object objObject, DateTime outTime)
    //    {
    //        cache.Set(CacheKey, objObject, outTime);
    //    }

    //    /// <summary>
    //    /// 移除指定数据缓存
    //    /// </summary>
    //    public static void Remove(string CacheKey)
    //    {
    //        cache.CacheRemove(CacheKey);
    //    }
    //}
}