using Xunit;
using Moq;
using System.Collections.Generic;
using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;
using Serilog;

namespace Infotecs.WebApi.Tests
{
    public class StatisticsServiceTests
    {
        [Fact]
        public void GetStatisticsIsEqualExpected()
        {
            // Arrange
            var expexted = new List<UserStatistics>();

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetStatisticsList()).Returns(expexted);

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
            var user = new Users() { ID = "001", name = "default" };
            var expexted = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetStatistics(user.ID)).Returns(expexted);

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
            Users user = new Users() { ID = "001", name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUser(user.ID)).Returns(user);

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
            Users user = new Users() { ID = "001", name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IRepository>();
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
            Users user = new Users() { ID = "001", name = "default" };
            UserStatistics statistics = new UserStatistics() { ID = user.ID };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUser(user.ID)).Returns(user);
            rep.Setup(a => a.GetStatistics(user.ID)).Returns(statistics);

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
            Users user = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
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
            Users user = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUser(user.ID)).Returns(user);
            var log = new Mock<ILogger>();

            StatisticsService statisticsService = new StatisticsService(rep.Object, log.Object);

            // Act
            int result = statisticsService.DeleteStatistics(user.ID);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
