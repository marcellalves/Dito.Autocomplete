using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Models;

namespace Dito.Autocomplete.Infrastructure.Services
{
    public interface IAutocompleteService
    {
        Task<IEnumerable<UserActivityResponse>> GetByTerm(string term);
    }

    public class AutocompleteService : IAutocompleteService
    {
        private readonly IAutocompleteRepository _autocompleteRepository;

        public AutocompleteService(IAutocompleteRepository autocompleteRepository)
        {
            _autocompleteRepository = autocompleteRepository;
        }

        public async Task<IEnumerable<UserActivityResponse>> GetByTerm(string term)
        {
            if (string.IsNullOrEmpty(term))
                throw new ArgumentNullException("O termo pesquisado não pode ser nulo ou vazio.");

            if (term.Length < 2)
                throw new ArgumentOutOfRangeException("Pesquise um termo com ao menos 2 caracteres.");

            var result = new List<UserActivityResponse>();

            var searchResult = await _autocompleteRepository.GetByTerm(term);
            if (searchResult != null && searchResult.Any())
                result = searchResult.ToList();

            return result;
        }
    }
}
