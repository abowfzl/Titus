using System.Collections.Generic;
using System.Net;
using StackExchange.Redis;

namespace Core.Redis
{
    public interface IRedisConnectionWrapper
    {
        IConnectionMultiplexer GetConnection();

        IDatabase GetDatabase(int db);

        IEnumerable<EndPoint> GetEndPoints();

        IServer GetServer(EndPoint endPoint);
    }
}