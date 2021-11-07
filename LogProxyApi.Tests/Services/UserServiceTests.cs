using LogProxyApi.Entities;
using LogProxyApi.Services;
using Xunit;

namespace LogProxyApi.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void ConstructorTest()
        {
            UserService userService = new UserService();

            Assert.NotNull(userService);
        }

        [Fact]
        public async void AuthenticateValidUser()
        {
            UserService userService = new UserService();

            User user = await userService.Authenticate("test", "helloworld");

            Assert.Equal(1, user.Id);
            Assert.Equal("test", user.Username);
            Assert.Null(user.Password);
        }

        [Fact]
        public async void AuthenticateInvalidUser()
        {
            UserService userService = new UserService();

            User user = await userService.Authenticate("test", "test");

            Assert.Null(user);
        }
    }
}