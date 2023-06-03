using UCABPagaloTodoMS.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Tests.MockData;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{

    public class LoginControllerTests
    {
        private readonly LoginController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<LoginController>> _loggerMock;

        public LoginControllerTests()
        {
            _loggerMock = new Mock<ILogger<LoginController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new LoginController(_loggerMock.Object, _mediatorMock.Object);

        }

        [Fact(DisplayName = "Login ADMIN OK")]
        public async Task Login_ADMIN_OK()
        {
            // Arrange
            UserLoginRequest request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "se170311"
            };
            var expectedResponse = BuildDataContextFaker.adminLoginResponse();
            
            _mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginQuery>(), default(CancellationToken)))
                            .ReturnsAsync(expectedResponse);
            // Act
            ActionResult result = await _controller.Login(request);
            OkObjectResult okObject = result as OkObjectResult;

            // Assert

            var response = Assert.IsType<UserLoginResponse>(okObject.Value);

            Assert.NotNull(result);
            Assert.NotNull(okObject);
            Assert.IsType<OkObjectResult>(result);
            
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            
            Assert.Equal(expectedResponse.Success, response.Success);
            Assert.Equal(expectedResponse.Status, response.Status);
            Assert.Equal(expectedResponse.isAdmin, response.isAdmin);
            Assert.Equal(expectedResponse.Message, response.Message);
        }
        [Fact(DisplayName = "Login USER OK")]
        public async Task Login_user_OK()
        {
            // Arrange
            UserLoginRequest request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "se170311"
            };
            var expectedResponse = BuildDataContextFaker.userLoginResponseStatusTrue();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginQuery>(), default(CancellationToken)))
                            .ReturnsAsync(expectedResponse);
            // Act
            ActionResult result = await _controller.Login(request);
            OkObjectResult okObject = result as OkObjectResult;

            // Assert

            var response = Assert.IsType<UserLoginResponse>(okObject.Value);

            Assert.NotNull(result);
            Assert.NotNull(okObject);
            Assert.IsType<OkObjectResult>(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);

            Assert.Equal(expectedResponse.Success, response.Success);
            Assert.Equal(expectedResponse.Status, response.Status);
            Assert.Equal(expectedResponse.isAdmin, response.isAdmin);
            Assert.Equal(expectedResponse.Message, response.Message);
        }
        [Fact(DisplayName = "Login USER BAN")]
        public async Task Login_user_BAN()
        {
            // Arrange
            UserLoginRequest request = new UserLoginRequest
            {
                UserName = "SedetC",
                Password = "se170311"
            };
            var expectedResponse = BuildDataContextFaker.userLoginResponseStatusFalse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginQuery>(), default(CancellationToken)))
                            .ReturnsAsync(expectedResponse);
            // Act
            ActionResult result = await _controller.Login(request);
            OkObjectResult okObject = result as OkObjectResult;

            // Assert

            var response = Assert.IsType<UserLoginResponse>(okObject.Value);

            Assert.NotNull(result);
            Assert.NotNull(okObject);
            Assert.IsType<OkObjectResult>(result);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);

            Assert.Equal(expectedResponse.Success, response.Success);
            Assert.Equal(expectedResponse.Status, response.Status);
            Assert.Equal(expectedResponse.isAdmin, response.isAdmin);
            Assert.Equal(expectedResponse.Message, response.Message);
        }

        [Fact(DisplayName = "Usuario no valido -->BadRequest")]
        public async Task Login_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserLoginRequest { UserName = "SedetC", Password = "se170311" };
            _mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginQuery>(), default)).ThrowsAsync(new ArgumentException("Invalid credentials"));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var badRequestResult = (BadRequestObjectResult)result;

            Assert.IsType<BadRequestObjectResult>(result);
            
            Assert.Equal("Invalid credentials", badRequestResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact(DisplayName = "excepcion no controlado")]
        public async Task Login_ThrowsException()
        {
            // Arrange
            var request = new UserLoginRequest { UserName = "SedetC", Password = "se170311" };
            _mediatorMock.Setup(x => x.Send(It.IsAny<UserLoginQuery>(), default)).ThrowsAsync(new Exception());

            // Act

            Func<Task> action = async () => await _controller.Login(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
                     

        }
    
       
    }
}