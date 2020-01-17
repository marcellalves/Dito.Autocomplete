using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest;

namespace Dito.Autocomplete.Models
{
    public class UserActivityRequest
    {
        public string Event { get; set; }
        public string TimeStamp { get; set; }
    }

    [ElasticsearchType(RelationName = "_doc")]
    public class UserActivityResponse
    {
        [Completion]
        [PropertyName("Event")]
        public string Event { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }

    public class UserActivity
    {
        public UserActivity()
        {
        }

        public UserActivity(string userEvent, string timeStamp)
        {
            Event = userEvent;
            TimeStamp = timeStamp;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Event { get; private set; }
        public string TimeStamp { get; private set; }
    }
}
