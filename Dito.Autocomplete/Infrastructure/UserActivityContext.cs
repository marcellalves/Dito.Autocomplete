using Dito.Autocomplete.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Dito.Autocomplete.Infrastructure
{
    public class UserActivityContext
    {
        private readonly IMongoDatabase _db;

        public UserActivityContext(IOptions<MongoDbConfig> dbConfig)
        {
            var settings = new MongoClientSettings
            {
                Servers = new[]
                {
                    new MongoServerAddress("mongo0", 30000),
                    new MongoServerAddress("mongo1", 30001),
                    new MongoServerAddress("mongo2", 30002)
                },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = "rs0"
            };

            //var client = new MongoClient(dbConfig.Value.ConnectionString);
            var client = new MongoClient(settings); 
            _db = client.GetDatabase(dbConfig.Value.Database);
        }

        public IMongoCollection<UserActivity> UserActivities => _db.GetCollection<UserActivity>("UserActivities");
    }
}
