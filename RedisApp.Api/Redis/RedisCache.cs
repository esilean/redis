using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RedisApp.Api.Redis
{
    public static class RedisCache
    {
        // save 
        public static async Task SetObjectAsync<T>(IDistributedCache cache, string key, T value)
        {
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value));
        }
        // get
        public static async Task<T> GetObjectAsync<T>(IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        // verify if an object exists
        public static async Task<bool> ExistObjectAsync(IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            return value != null;
        }
    }
}
