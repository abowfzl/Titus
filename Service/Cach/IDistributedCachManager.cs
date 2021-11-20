using System.Collections.Generic;
using StackExchange.Redis;

namespace Service.Cach
{
    public interface IDistributedCachManager
    {
        T GetKey<T>(string key);

        void SetKey(string key, object data, int expiresInMinutes);

        bool IsKeyExist(string key);

        void Remove(string key);

        List<RedisKey> GetAllKeys();
    }
}