using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core.Redis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Service.Cach
{
    public class RedisCachManager : IDistributedCachManager
    {
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;


        public RedisCachManager(IRedisConnectionWrapper connectionWrapper, IConfiguration config)
        {
            _connectionWrapper = connectionWrapper;
            _db = _connectionWrapper.GetDatabase(int.Parse(config.GetSection("RedisConfig")["RedisDatabaseId"]));
        }


        public T GetKey<T>(string key)
        {
            var serializedItem = _db.StringGet(key);

            if (!serializedItem.HasValue)
                return default(T);

            var item = JsonConvert.DeserializeObject<T>(serializedItem);

            return item ?? default(T);
        }

        public void SetKey(string key, object data, int expiresInMinutes)
        {
            if (data == null)
                return;

            var expiresIn = TimeSpan.FromMinutes(expiresInMinutes);

            var serializedItem = JsonConvert.SerializeObject(data);

            _db.StringSet(key, serializedItem, expiresIn);
        }

        public bool IsKeyExist(string key)
        {
            return _db.KeyExists(key);
        }

        public virtual List<RedisKey> GetAllKeys()
        {
            var allRedisKeys = new List<RedisKey>();

            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var keys = GetKeys(endPoint).ToArray();

                allRedisKeys.AddRange(keys);
            }

            return allRedisKeys.Distinct().ToList();
        }

        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        protected virtual IEnumerable<RedisKey> GetKeys(EndPoint endPoint)
        {
            var server = _connectionWrapper.GetServer(endPoint);

            var keys = server.Keys(_db.Database);

            return keys;
        }
    }
}