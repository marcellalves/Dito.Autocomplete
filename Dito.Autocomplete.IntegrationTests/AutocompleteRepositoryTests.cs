using System.Linq;
using System.Threading.Tasks;
using Dito.Autocomplete.Infrastructure.Repositories;
using Xunit;

namespace Dito.Autocomplete.IntegrationTests
{
    public class AutocompleteRepositoryTests
    {
        [Fact]
        public async Task GetByTerm_ExistingValidSearchTerm_ListOfOccurrences()
        {
            var repository = new AutocompleteRepository();

            var result = await repository.GetByTerm("bu");

            Assert.True(result.Any());
        }
    }
}
