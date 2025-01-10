using Microsoft.AspNetCore.Identity;
using Moq;
using MyTask_Management_System.Core.Model;
using MyTask_Management_System.Core.Repository;
using MyTask_Management_System.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTask_Management_System.Dto;

namespace MyTask_Management_System.test
{
    internal class AccountControllerTests
    {
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AcountController _controller;

        public AccountControllerTests()
        {
            var store = new Mock<IUserStore<AppUser>>();
            _userManagerMock = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            _tokenServiceMock = new Mock<ITokenService>();
            _controller = new AcountController(_userManagerMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsUserDto()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            var user = new AppUser { Email = "test@example.com", Name = "Test User" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.CreateToken(It.IsAny<AppUser>())).ReturnsAsync("sampleToken");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDto>>(result);
            var userDto = Assert.IsType<UserDto>(actionResult.Value);
            Assert.Equal("test@example.com", userDto.Email);
            Assert.Equal("Test User", userDto.Name);
            Assert.Equal("sampleToken", userDto.Token);
        }

        [Fact]
        public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "wrongPassword" };
            var user = new AppUser { Email = "test@example.com" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }
    }
}
