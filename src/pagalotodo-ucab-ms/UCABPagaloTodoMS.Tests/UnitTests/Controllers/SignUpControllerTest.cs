using Castle.Core.Logging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class SignUpControllerTest
    {
        private readonly SignUpController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<SignUpController>> _loggerMock;
        public SignUpControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<SignUpController>>();
            _controller = new SignUpController(_loggerMock.Object,_mediatorMock.Object);

        }
        [Fact(DisplayName = "SignUp User ok")]
        public async Task signupuserok()
        {
            //Arrage
            var request = BuildDataSignUpContextFaker.userSignUpRequest();
            var expectedResponse = Guid.NewGuid();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserSignUpCommand>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.signupuser(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "SignUp User exception")]
        public async Task signupEx()
        {
            //Arrage
            var request =  BuildDataSignUpContextFaker.userSignUpRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserSignUpCommand>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.signupuser(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }

        [Fact(DisplayName ="SignUp Provider ok")]
        public async Task signupprovideerOK()
        {
            //Arrage
            var request = BuildDataSignUpContextFaker.ProviderSignUpRequest();
            var expectedResponse = Guid.NewGuid();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ProviderSignUpCommand>(), default(CancellationToken)))
                .ReturnsAsync(expectedResponse);
            //Act
            var result = await _controller.signupprovider(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        }
        [Fact(DisplayName = "SignUp Provider exception")]
        public async Task signupproviderEx()
        {
            //Arrage
            var request = BuildDataSignUpContextFaker.ProviderSignUpRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ProviderSignUpCommand>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.signupprovider(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
    }
    

}
