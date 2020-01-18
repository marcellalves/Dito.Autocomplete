using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dito.Autocomplete.Models;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ElasticsearchConfig> _esConfig;

        public AutocompleteRepository(IOptions<ElasticsearchConfig> esConfig)
        {
            _esConfig = esConfig;
            _node = new Uri(esConfig.Value.ElasticsearchUrl);
            _settings = new ConnectionSettings(_node)
                .DefaultIndex(esConfig.Value.IndexName)
                .DefaultFieldNameInferrer(p => p);
            _settings.DisableDirectStreaming();
            _client = new ElasticClient(_settings);
        }

        public async Task<IEnumerable<UserActivityResponse>> GetByTerm(string term)
        {
            ISearchResponse<UserActivity> searchResponse = await _client.SearchAsync<UserActivity>(s => s
                .Index(_esConfig.Value.IndexName)
                .Suggest(su => su
                    .Completion("suggestions", c => c
                        .Field(f => f.Event)
                        .Prefix(term)
                        .Fuzzy(f => f
                            .Fuzziness(Fuzziness.Auto)
                        )
                        .Size(5))
                ));

            var suggests = from suggest in searchResponse.Suggest["suggestions"]
                from option in suggest.Options
                select new UserActivityResponse
                {
                    Event = option.Source.Event,
                    Score = option.Score
                };

            return suggests;
        }
    }
}
