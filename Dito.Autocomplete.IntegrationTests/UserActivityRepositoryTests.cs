using Dito.Autocomplete.Controllers;
using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Dito.Autocomplete.IntegrationTests
{
    public class UserActivityRepositoryTests
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public UserActivityRepositoryTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            var startup = new Startup(_configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
        }

        //[Fact]
        //public async Task should_add_new_user_activity()
        //{
        //    using(var scope = _scopeFactory.CreateScope())
        //    {
        //        //var repo = scope.ServiceProvider.GetService<IUserActivityRepository>();

        //        //var newUserActivity = new UserActivity("buy", "2016-09-22T13:57:31.2311892-04:00");

        //        //var insertedId = await repo.Create(newUserActivity);
        //        //var expected = await repo.GetById(insertedId);

        //        //Assert.Equal(expected.Event, newUserActivity.Event);
        //    }
        //}
    }
}
