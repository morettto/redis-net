namespace ProductApi.Domain.Interfaces.Redis
{
    public interface IRedisService
    {
        Task SetAsync<T>(string key, T value);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
