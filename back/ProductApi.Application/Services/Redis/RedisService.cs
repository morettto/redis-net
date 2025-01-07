using Microsoft.Extensions.Caching.Distributed;
using ProductApi.Domain.Interfaces.Redis;
using System.Text.Json;

namespace ProductApi.Application.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _redis;
        private readonly DistributedCacheEntryOptions _options;

        public RedisService(IDistributedCache redis)
        {
            _redis = redis;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(10)
            };
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _redis.SetStringAsync(key, json, _options);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _redis.GetStringAsync(key);
            return !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<T>(value) : default;
        }

        public async Task RemoveAsync(string key)
        {
             await _redis.RemoveAsync(key);
        }
    }

}
