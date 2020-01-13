using Dito.Autocomplete.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Dito.Autocomplete.Infrastructure
{
    public class UserActivityContext
    {
        private readonly IMongoDatabase _db = null;

        public UserActivityContext(IOptions<MongoDbConfig> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.ConnectionString);
            if (client != null)
                _db = client.GetDatabase(dbConfig.Value.Database);
            else
                throw new Exception("Banco de dados fora do ar.");
        }

        public IMongoCollection<UserActivity> UserActivities => _db.GetCollection<UserActivity>("UserActivities");
    }
}
