using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dito.Autocomplete.Models;
using Nest;

namespace Dito.Autocomplete.Infrastructure.Repositories
{
    public interface IAutocompleteRepository
    {
        Task<IEnumerable<UserActivityResponse>> GetByTerm(string term);
    }

    public class AutocompleteRepository : IAutocompleteRepository
    {
        private readonly Uri _node;
        private readonly ConnectionSettings _settings;
        private readonly ElasticClient _client;

        public AutocompleteRepository()
        {
            _node = new Uri("http://es7:9200");
            _settings = new ConnectionSettings(_node).DefaultIndex("autocomplete_index");
            _settings.DisableDirectStreaming(true);
            _client = new ElasticClient(_settings);
        }

        public async Task<IEnumerable<UserActivityResponse>> GetByTerm(string term)
        {
            var searchResponse = await _client.SearchAsync<UserActivityResponse>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Event)
                        .Query(term)
                        .Analyzer("standard"))));

            return searchResponse.Documents;
        }
    }
}
