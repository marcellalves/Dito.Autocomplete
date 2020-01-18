using System;
using Dito.Autocomplete.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Dito.Autocomplete.Infrastructure
{
    public class UserActivityContext
    {
        private readonly IMongoDatabase _db;
        private readonly IOptions<MongoDbConfig> _dbConfig;

        public UserActivityContext(IOptions<MongoDbConfig> dbConfig)
        {
            _dbConfig = dbConfig;

            var settings = new MongoClientSettings
            {
                Servers = new[]
                {
                    new MongoServerAddress(_dbConfig.Value.Node1Name, Convert.ToInt32(_dbConfig.Value.Node1Port)),
                    new MongoServerAddress(_dbConfig.Value.Node2Name, Convert.ToInt32(_dbConfig.Value.Node2Port)),
                    new MongoServerAddress(_dbConfig.Value.Node3Name, Convert.ToInt32(_dbConfig.Value.Node3Port))
                },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = _dbConfig.Value.ReplicaSetName
            };

            var client = new MongoClient(settings); 
            _db = client.GetDatabase(_dbConfig.Value.Database);
        }

        public IMongoCollection<UserActivity> UserActivities => _db.GetCollection<UserActivity>(_dbConfig.Value.CollectionName);
    }
}
