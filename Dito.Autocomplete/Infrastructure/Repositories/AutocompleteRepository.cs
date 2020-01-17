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
            //_client.Map<UserActivityRequestES>(m => m
            //    .AutoMap()
            //    .Properties(ps => ps
            //        .Text(t => t.Name(n => n.Event)
            //            .Su)));
        }

        public async Task<IEnumerable<UserActivityResponse>> GetByTerm(string term)
        {
            //var response = _client.Map<UserActivityRequestES>(m => m
            //    .Index("useractivitydb.useractivities")
            //    .Properties(props => props
            //        .Number(n => n
            //            .Name(p => p.ID)
            //            .Type(Type.)
            //        )
            //    )
            //);

            //var searchResponse = await _client.SearchAsync<UserActivityRequestES>(s => s
            //    .Suggest(scd => scd
            //        .Completion("user-activities-completion", cs => cs
            //            .Prefix(term)
            //            .Fuzzy(fsd => fsd
            //                .Fuzziness(Fuzziness.Auto))
            //            .Field(f => f.Event))));

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
