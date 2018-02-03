using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Association.Helpers
{
    public static class RedisHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["RedisCacheConnection"].ConnectionString);
        });

        private static StackExchange.Redis.ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        public static async Task<string> GetValue(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            string result = await cache.StringGetAsync(key);
            return result;
        }
        public static async Task<bool> SetValue(string key, string value)
        {
            IDatabase cache = Connection.GetDatabase();
            var result = await cache.StringSetAsync(key, value);
            return result;
        }
        public static async Task<bool> SetValue(string key, string value, int minutes)
        {
            IDatabase cache = Connection.GetDatabase();
            var result = await cache.StringSetAsync(key, value, TimeSpan.FromMinutes(minutes));
            return result;
        }
        public static async Task<bool> Delete(string key)
        {
            IDatabase cache = Connection.GetDatabase();
            var result = await cache.KeyDeleteAsync(key);
            return result;
        }
    }
}