using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Infrastructure.Services;
using Dito.Autocomplete.Models;
using Moq;
using Xunit;

namespace Dito.Autocomplete.UnitTests
{
    public class AutocompleteServiceTests
    {
        [Fact]
        public async Task GetByTerm_ExistingValidSearchTerm_ListOfOccurrences()
        {
            var repository = new Mock<IAutocompleteRepository>();

            var existingValidSearchTerm = "bu";

            repository.Setup(r => r.GetByTerm(existingValidSearchTerm)).ReturnsAsync(new List<UserActivityResponse>
            {
                new UserActivityResponse { Event = "buy", TimeStamp = new DateTimeOffset(DateTime.Now) },
                new UserActivityResponse { Event = "buy",  TimeStamp = new DateTimeOffset(DateTime.Now) },
                new UserActivityResponse { Event = "build",  TimeStamp = new DateTimeOffset(DateTime.Now) }
            });

            var service = new AutocompleteService(repository.Object);

            var result = await service.GetByTerm(existingValidSearchTerm);

            Assert.Equal(3, result.ToList().Count);
        }

        [Fact]
        public async Task GetByTerm_NonExistingValidSearchTerm_EmptyList()
        {
            var repository = new Mock<IAutocompleteRepository>();

            repository.Setup(r => r.GetByTerm("bu")).ReturnsAsync(new List<UserActivityResponse>
            {
                new UserActivityResponse { Event = "buy", TimeStamp = new DateTimeOffset(DateTime.Now) },
                new UserActivityResponse { Event = "buy", TimeStamp = new DateTimeOffset(DateTime.Now) },
                new UserActivityResponse { Event = "build", TimeStamp = new DateTimeOffset(DateTime.Now) }
            });

            var service = new AutocompleteService(repository.Object);

            var result = await service.GetByTerm("xx");

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByTerm_OneCharacterTerm_ArgumentOutOfRangeException()
        {
            var repository = new Mock<IAutocompleteRepository>();

            repository.Setup(r => r.GetByTerm(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IEnumerable<UserActivityResponse>>());

            var service = new AutocompleteService(repository.Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetByTerm("x"));
        }

        [Fact]
        public async Task GetByTerm_NullTerm_ArgumentNullException()
        {
            var repository = new Mock<IAutocompleteRepository>();

            repository.Setup(r => r.GetByTerm(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IEnumerable<UserActivityResponse>>());

            var service = new AutocompleteService(repository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetByTerm(null));
        }

        [Fact]
        public async Task GetByTerm_EmptyTerm_ArgumentNullException()
        {
            var repository = new Mock<IAutocompleteRepository>();

            repository.Setup(r => r.GetByTerm(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IEnumerable<UserActivityResponse>>());

            var service = new AutocompleteService(repository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetByTerm(string.Empty));
        }
    }
}
