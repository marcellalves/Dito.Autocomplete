using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Infrastructure.Services;
using Dito.Autocomplete.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dito.Autocomplete.UnitTests
{
    public class UserActivityServiceTests
    {
        [Fact]
        public async Task GetAll_Returns_AllUserActivities()
        {
            var repository = new Mock<IUserActivityRepository>();

            repository.Setup(r => r.GetAll()).ReturnsAsync(new List<UserActivity> { 
                new UserActivity("buy", "2016-09-22T13:57:31.2311892-04:00"),
                new UserActivity("click", "2016-09-23T22:50:11.2311892-04:00"),
                new UserActivity("scroll", "2016-08-30T12:11:22.5671892-04:00"),
            });

            var service = new UserActivityService(repository.Object);
            var result = await service.GetAll();

            Assert.Equal(3, result.ToList().Count);
        }

        [Fact]
        public async Task GetById_ExistingDocumentId_RelatedDocument()
        {
            var repository = new Mock<IUserActivityRepository>();

            const string existingId = "5e1b9429757df200173c6c36";
            const string userEvent = "buy";
            const string timeStamp = "2016-09-22T13:57:31.2311892-04:00";

            repository.Setup(r => r.GetById(existingId))
                .ReturnsAsync(new UserActivity(userEvent, timeStamp));

            var service = new UserActivityService(repository.Object);

            var document = await service.GetById(existingId);

            Assert.Equal(userEvent, document.Event);
            Assert.Equal(timeStamp, document.TimeStamp);
        }

        [Fact]
        public async Task GetById_NonExistingDocumentId_Null()
        {
            var repository = new Mock<IUserActivityRepository>();

            repository.Setup(r => r.GetById("5e1b9429757df200173c6c36"))
                .ReturnsAsync(new UserActivity("buy", "2016-09-22T13:57:31.2311892-04:00"));

            var service = new UserActivityService(repository.Object);

            var document = await service.GetById("999999999999999999999999");

            Assert.Null(document);
        }

        [Fact]
        public async Task GetById_NullId_ArgumentNullException()
        {
            var repository = new Mock<IUserActivityRepository>();

            repository.Setup(r => r.GetById(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<UserActivity>());

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetById(null));
        }

        [Fact]
        public async Task GetById_EmptyId_ArgumentException()
        {
            var repository = new Mock<IUserActivityRepository>();

            repository.Setup(r => r.GetById(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<UserActivity>());

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetById(string.Empty));
        }

        [Fact]
        public async Task Create_ValidData_InsertedDocumentId()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            var insertedDocumentId = await service.Create(new UserActivityRequest
            {
                Event = "buy",
                TimeStamp = "2016-09-22T13:57:31.2311892-04:00"
            });

            Assert.Equal(expectedDocumentId, insertedDocumentId);
        }

        [Fact]
        public async Task Create_NullObject_ArgumentNullException()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.Create(null));
        }

        [Fact]
        public async Task Create_NullEvent_ArgumentException()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => 
            service.Create(new UserActivityRequest
            {
                Event = null,
                TimeStamp = "2016-09-22T13:57:31.2311892-04:00"
            }));
        }

        [Fact]
        public async Task Create_EmptyEvent_ArgumentException()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
            service.Create(new UserActivityRequest
            {
                Event = string.Empty,
                TimeStamp = "2016-09-22T13:57:31.2311892-04:00"
            }));
        }

        [Fact]
        public async Task Create_NullTimeStamp_ArgumentException()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
            service.Create(new UserActivityRequest
            {
                Event = "buy",
                TimeStamp = null
            }));
        }

        [Fact]
        public async Task Create_EmptyTimeStamp_ArgumentException()
        {
            var repository = new Mock<IUserActivityRepository>();

            var expectedDocumentId = "5e1b9429757df200173c6c36";

            repository.Setup(r => r.Create(It.IsAny<UserActivity>())).ReturnsAsync(expectedDocumentId);

            var service = new UserActivityService(repository.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
            service.Create(new UserActivityRequest { Event = "buy", TimeStamp = string.Empty }));
        }
    }
}
