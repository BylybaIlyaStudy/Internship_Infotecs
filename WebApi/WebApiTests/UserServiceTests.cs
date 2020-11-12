using Xunit;
using Moq;
using System.Collections.Generic;
using Infotecs.WebApi.Models;
using Infotecs.WebApi.Services;
using Serilog;

namespace Infotecs.WebApi.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUsersListIsEqualExpected()
        {
            // Arrange
            var expexted = new List<Users>();

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUsersList()).Returns(expexted);

            var log = new Mock<ILogger>();

            UserService userService = new UserService(rep.Object, log.Object);

            // Act
            List<Users> result = userService.GetUsersList();

            // Assert
            Assert.Equal(expexted, result);
        }

        [Fact]
        public void GetUserByIdIsEqualExpected()
        {
            // Arrange
            var expexted = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUser(expexted.ID)).Returns(expexted);

            var log = new Mock<ILogger>();

            UserService userService = new UserService(rep.Object, log.Object);

            // Act
            Users result = userService.GetUser(expexted.ID);

            // Assert
            Assert.Equal(expexted, result);
        }

        [Fact]
        public void CerateUserIsComplete()
        {
            // Arrange
            int expected = 200;
            Users user = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
            var log = new Mock<ILogger>();

            UserService userService = new UserService(rep.Object, log.Object);

            // Act
            int result = userService.CreateUser(user);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CerateUserCloneIsIncomplete()
        {
            // Arrange
            int expected = 412;
            Users user = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
            rep.Setup(a => a.GetUser(user.ID)).Returns(user);

            var log = new Mock<ILogger>();

            UserService userService = new UserService(rep.Object, log.Object);

            // Act
            int result = userService.CreateUser(user);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteUserNotExistIsIncomplete()
        {
            // Arrange
            int expected = 404;
            Users user = new Users() { ID = "001", name = "default" };

            var rep = new Mock<IRepository>();
            var log = new Mock<ILogger>();

            UserService userService = new UserService(rep.Object, log.Object);

            // Act
            int result = userService.DeleteUser(user.ID);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
