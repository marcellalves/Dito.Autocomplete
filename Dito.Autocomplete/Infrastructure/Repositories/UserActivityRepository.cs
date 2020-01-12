using Dito.Autocomplete.Models;
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
        private readonly IUserActivityContext _context;

        public UserActivityRepository(IUserActivityContext context)
        {
            _context = context;
        }

        public async Task<long> GetNextId()
        {
            return await _context.UserActivities.CountDocumentsAsync(new BsonDocument()) + 1;
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
