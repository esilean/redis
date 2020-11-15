using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisApp.Api.Models;
using RedisApp.Api.Redis;
using RedisApp.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Api.Services
{
    public class Momo : IMomo
    {

        private const string MOMO_KEY = "momokey";

        private readonly IDistributedCache _cache;

        public Momo(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<MomoModel>> GetMomo(int id)
        {
            var dataCache = await _cache.GetStringAsync(MOMO_KEY);

            if (string.IsNullOrWhiteSpace(dataCache))
            {
                var momos = GetAllMomos();
                await Task.Delay(2000);
                var momoToJson = JsonConvert.SerializeObject(momos);

                var cacheSettings = new DistributedCacheEntryOptions();
                cacheSettings.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                await _cache.SetStringAsync(MOMO_KEY, momoToJson, cacheSettings);

                return momos;
            }

            var momosFromCache = JsonConvert.DeserializeObject<List<MomoModel>>(dataCache);

            if (id == 4)
                return momosFromCache.Where(x => x.Id == id);

            return momosFromCache;
        }

        public async Task<IEnumerable<MomoModel>> GetMomoAU(int id)
        {
            if (await RedisCache.ExistObjectAsync(_cache, MOMO_KEY))
            {
                var momosFromCache = await RedisCache.GetObjectAsync<IEnumerable<MomoModel>>(_cache, MOMO_KEY);

                if (id == 4)
                    return momosFromCache.Where(x => x.Id == id);

                return momosFromCache;
            }
            else
            {
                var momos = GetAllMomos();
                await Task.Delay(2000);

                await RedisCache.SetObjectAsync<IEnumerable<MomoModel>>(_cache, MOMO_KEY, momos);
                return momos;
            }
        }

        public List<MomoModel> GetAllMomos()
        {
            return new List<MomoModel>
            {
                new MomoModel { Id = 1, Description = "Description 1" },
                new MomoModel { Id = 2, Description = "Description 2" },
                new MomoModel { Id = 3, Description = "Description 3" },
                new MomoModel { Id = 4, Description = "Description 4" }
            };
        }
    }
}
