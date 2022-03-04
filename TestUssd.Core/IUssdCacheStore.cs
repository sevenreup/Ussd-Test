using System.Threading.Tasks;

namespace TestUssd.Core
{
    public interface IUssdCacheStore
    {
        public Task<bool> ValueExists(string key);
        public Task Set(string key, string value);
        public  Task<string> GetValue(string key);
        public  Task Delete(string nextRouteKey);
    }
}