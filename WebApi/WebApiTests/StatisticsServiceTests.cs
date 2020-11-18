using Xunit;
using Moq;
using System.Collections.Generic;
using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;
using Serilog;
using WebApi.Repositories;
using Infotecs.WebApi.Repositories;

namespace Infotecs.WebApi.Tests
{
    public class StatisticsServiceTests
    {
        [Fact]
        public void GetStatisticsIsEqualExpected()
        {
            // Arrange
            var expexted = new List<UserStatistics>();

            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Statistics.GetList()).Returns(expexted);

            var log = new Mock<ILogger>();

            StatisticsService userService = new StatisticsService(rep.Object, log.Object);

            // Act
            List<UserStatistics> result = userService.GetStatistics();

            // Assert
            Assert.Equal(expexted, result);
        }

        [Fact]
        public void GetStatisticsByIdIsEqualExpected()
        {
            // Arrange
            var user = new Users() { ID = "001", Name = "default" };
            UserStatistics expexted = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IUnitOfWork>();
            
            rep.Setup(a => a.Statistics.Get(user.ID)).Returns(expexted);
            rep.Setup(a => a.Events.Get(user.ID)).Returns(new List<Events>());

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            UserStatistics result = statisticsService.GetStatistics(user.ID);

            // Assert
            Assert.Equal(expexted, result);
        }

        [Fact]
        public void CerateStatisticsIsComplete()
        {
            // Arrange
            int expected = 200;
            Users user = new Users() { ID = "001", Name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var mow = new Mock<IRepository<UserStatistics>>();
            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Statistics).Returns(mow.Object);

            rep.Setup(a => a.Users.Get(user.ID)).Returns(user);
            rep.Setup(a => a.Events.Get(user.ID)).Returns(new List<Events>());

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.CreateStatistics(statistics);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CerateStatisticsNotExistUserIsIncomplete()
        {
            // Arrange
            int expected = 404;
            Users user = new Users() { ID = "001", Name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var mow = new Mock<IRepository<Users>>();
            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Users).Returns(mow.Object);

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.CreateStatistics(statistics);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteStatisticsIsComplete()
        {
            // Arrange
            int expected = 200;
            Users user = new Users() { ID = "001", Name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Users.Get(user.ID)).Returns(user);
            rep.Setup(a => a.Statistics.Get(user.ID)).Returns(statistics);
            rep.Setup(a => a.Events.Get(user.ID)).Returns(new List<Events>());

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.DeleteStatistics(user.ID);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteStatisticsNotExistUserIsIncomplete()
        {
            // Arrange
            int expected = 404;
            Users user = new Users() { ID = "001", Name = "default" };

            var mow = new Mock<IRepository<Users>>();
            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Users).Returns(mow.Object);

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.DeleteStatistics(user.ID);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteStatisticsNotExistStatisticsIsIncomplete()
        {
            // Arrange
            int expected = 404;
            Users user = new Users() { ID = "001", Name = "default" };

            var mow = new Mock<IRepository<UserStatistics>>();
            var rep = new Mock<IUnitOfWork>();
            rep.Setup(a => a.Statistics).Returns(mow.Object);
            rep.Setup(a => a.Users.Get(user.ID)).Returns(user);

            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.DeleteStatistics(user.ID);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
