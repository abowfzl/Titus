using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Core.Redis
{
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {
        private readonly IConfiguration _config;
        private readonly object _lock = new object();
        private readonly string _connectionString;
        private volatile ConnectionMultiplexer _connection;


        #region Ctor

        public RedisConnectionWrapper(IConfiguration config)
        {
            _config = config;
            _connectionString = GetConnectionString();
        }

        #endregion

        #region Utitlities

        private string GetConnectionString()
        {
            return _config.GetSection("RedisConfig")["RedisConnectionString"];
        }

        #endregion

        public IConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                //Connection disconnected. Disposing connection...
                _connection?.Dispose();

                //Creating new instance of Redis Connection
                _connection = ConnectionMultiplexer.Connect(_connectionString);
            }

            return _connection;
        }

        public IDatabase GetDatabase(int db)
        {
            return GetConnection().GetDatabase(db);
        }
        
        public IEnumerable<EndPoint> GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }
        
        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }
    }
}