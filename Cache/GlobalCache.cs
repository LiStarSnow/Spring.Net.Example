
/* ***********************************************
 * author :  
 * function: 全局缓存对象
 * history:  created by  2015/08/20
 * ***********************************************/
namespace Cache
{
    /// <summary>
    /// 全局缓存，GlobalCache
    /// </summary>
    public class GlobalCache
    {
        /// <summary>
        /// cache object
        /// </summary>
        public static readonly ICacheProvider<object> Cache = new LruMemoryCache<object>();
    }
}