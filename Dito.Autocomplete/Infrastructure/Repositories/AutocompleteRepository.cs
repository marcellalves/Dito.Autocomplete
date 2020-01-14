using System.Collections.Generic;
using System.Threading.Tasks;
using Dito.Autocomplete.Models;

namespace Dito.Autocomplete.Infrastructure.Repositories
{
    public interface IAutocompleteRepository
    {
        Task<IEnumerable<UserActivity>> GetByTerm(string term);
    }

    public class AutocompleteRepository : IAutocompleteRepository
    {
        public Task<IEnumerable<UserActivity>> GetByTerm(string term)
        {
            throw new System.NotImplementedException();
        }
    }
}
