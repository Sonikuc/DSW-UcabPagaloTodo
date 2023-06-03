using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Tests.DataSeed;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTestsApplication.Handlers.Queries
{
    public class UserLoginQueryHandlerTest
    {
        private readonly UserLoginQueryHandler _handler;
        private readonly Mock<IUCABPagaloTodoDbContext> _contextMock;
        private readonly Mock<ILogger<UserLoginQueryHandler>> _loggerMock;

        public UserLoginQueryHandlerTest()
        {
            var faker = new Faker();
            _contextMock = new Mock<IUCABPagaloTodoDbContext>();
            _loggerMock = new Mock<ILogger<UserLoginQueryHandler>>();
            _handler = new UserLoginQueryHandler(_contextMock.Object, _loggerMock.Object);
            _contextMock.SetupDbContextData();
        }
      
      /*  [Fact(DisplayName ="Login Correct")]
        public async Task HandleAsync_UserExiste_PasswordCorrecta()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "se170311"
            };
            var user = new UserLoginQuery(request);
            var expectedResponse = BuildDataContextFaker.userLoginResponse();

            // Act
            var response = await _handler.HandleAsync(user);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedResponse.UserName, response.UserName);
            Assert.Equal(expectedResponse.Password, response.Password);
            Assert.True(response.Success);
            Assert.Equal("Inicio de sesión exitoso", response.Message);
            Assert.Equal(expectedResponse.Id, response.Id);
        }*/
        [Fact(DisplayName = "Login Incorrect - Error Password")]
        public async Task HandleAsync_WrongPassword()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "wrongpassword"
            };
            var user = new UserLoginQuery(request);

            // Act
            var response = await _handler.HandleAsync(user);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Contraseña incorrecta", response.Message);
            Assert.Equal("", response.UserName);
            Assert.Equal("", response.Password);
        }

        [Fact(DisplayName = "Login Incorrect - User No Encontrado")]
        public async Task HandleAsync_UserNotFound()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "NonExistingUser",
                Password = "se170311"
            };
            var user = new UserLoginQuery(request);

            // Act
            var response = await _handler.HandleAsync(user);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Usuario no encontrado", response.Message);
            Assert.Equal("", response.UserName);
            Assert.Equal("", response.Password);
        }
        
        [Fact(DisplayName = "Login Incorrect - Database Error")]
        public async Task HandleAsync_DatabaseError()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "se170311"
            };
            var user = new UserLoginQuery(request);
            _contextMock.Setup(x => x.UserEntities).Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.HandleAsync(user));
        }

      /*  [Fact(DisplayName = "Login Incorrect - Request nulo ")]
        public async Task HandleAsync_RequestNull()
        {
            // Arrange
            UserLoginQuery user = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.HandleAsync(user));
        }

        /*[Fact(DisplayName = "Login Incorrect - UserName nulo o vacio ")]
        public async Task HandleAsync_UserNameNullOrEmpty()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "",
                Password = "se170311"
            };
            var user = new UserLoginQuery(request);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.HandleAsync(user));
        }

        [Fact(DisplayName = "Login Incorrect - Password nulo o vacio")]
        public async Task HandleAsync_PasswordNullOrEmpty()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = null
            };
            var user = new UserLoginQuery(request);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.HandleAsync(user));
        }*/


       
    }
}
