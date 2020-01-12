using Dito.Autocomplete.Models;
using MongoDB.Driver;

namespace Dito.Autocomplete.Infrastructure
{
    public interface IUserActivityContext
    {
        IMongoCollection<UserActivity> UserActivities { get; }
    }

    public class UserActivityContext : IUserActivityContext
    {
        private readonly IMongoDatabase _db;

        public UserActivityContext(MongoDbConfig config)
        {
            //var client = new MongoClient(config.ConnectionString);
            var client = new MongoClient("mongodb://mongo");
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<UserActivity> UserActivities => _db.GetCollection<UserActivity>("UserActivities");
    }
}
