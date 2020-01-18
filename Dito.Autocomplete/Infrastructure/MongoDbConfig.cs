namespace Dito.Autocomplete.Infrastructure
{
    public class MongoDbConfig
    {
        public string Node1Name { get; set; }
        public string Node2Name { get; set; }
        public string Node3Name { get; set; }
        public string Node1Port { get; set; }
        public string Node2Port { get; set; }
        public string Node3Port { get; set; }
        public string ReplicaSetName { get; set; }
        public string Database { get; set; }
        public string CollectionName { get; set; }
    }
}
