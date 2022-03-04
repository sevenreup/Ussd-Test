using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace TestUssd.Core
{
    public class RedisStore : IUssdCacheStore
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisStore(IConnectionMultiplexer connection)
        {
            _redis = connection;
            _db = connection.GetDatabase();
        }

        public async Task<bool> ValueExists(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        public async Task Set(string key, string value)
        {
            await _db.StringSetAsync(key, value);
            await ResetExpiry(key);
        }

        public async Task<string> GetValue(string key)
        {
            var value = await _db.StringGetAsync(key);
            await ResetExpiry(key);
            return value;
        }

        private async Task ResetExpiry(string key)
        {
            await _db.KeyExpireAsync(key, TimeSpan.FromSeconds(90));
        }

        public async Task Delete(string nextRouteKey)
        {
            await _db.KeyDeleteAsync(nextRouteKey);
        }
    }
}
