using Dito.Autocomplete.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dito.Autocomplete.Infrastructure.Repositories
{
    public interface IUserActivityRepository
    {
        Task<IEnumerable<UserActivity>> GetAll();
        Task<UserActivity> GetById(string id);
        Task<string> Create(UserActivity model);
    }

    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly UserActivityContext _context;

        public UserActivityRepository(IOptions<MongoDbConfig> dbConfig)
        {
            _context = new UserActivityContext(dbConfig);
        }

        public async Task<string> Create(UserActivity model)
        {
            await _context.UserActivities.InsertOneAsync(model);
            return model.Id;
        }

        public async Task<IEnumerable<UserActivity>> GetAll()
        {
            return await _context.UserActivities
                .Find(new BsonDocument())
                .ToListAsync();
        }

        public async Task<UserActivity> GetById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            return await _context.UserActivities
                .Find(filter.ToBsonDocument())
                .FirstAsync();
        }
    }
}
