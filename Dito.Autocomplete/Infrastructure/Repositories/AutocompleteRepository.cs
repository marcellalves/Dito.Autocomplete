using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dito.Autocomplete.Models;
using Nest;

namespace Dito.Autocomplete.Infrastructure.Repositories
{
    public interface IAutocompleteRepository
    {
        Task<IEnumerable<object>> GetByTerm(string term);
    }

    public class AutocompleteRepository : IAutocompleteRepository
    {
        private readonly Uri _node;
        private readonly ConnectionSettings _settings;
        private readonly ElasticClient _client;

        public AutocompleteRepository()
        {
            _node = new Uri("http://localhost:9200");
            _settings = new ConnectionSettings(_node).DefaultIndex("useractivitydb.useractivities");
            _client = new ElasticClient(_settings);
        }

        public async Task<IEnumerable<object>> GetByTerm(string term)
        {
            var searchResponse = await _client.SearchAsync<object>(s => s
                .Query(q => q
                    .Prefix(c => c
                        .Name("autocomplete")
                        .Boost(1.1)
                        .Field("Event")
                        .Value(term)
                        .Rewrite(MultiTermQueryRewrite.TopTerms(10)))));

            return searchResponse.Documents;
        }
    }
}
