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
    public class UserUpdateControllerTest
    {

        private readonly UserUpdateController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UserUpdateController>> _loggerMock;

        public UserUpdateControllerTest()
        {
            _loggerMock = new Mock<ILogger<UserUpdateController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserUpdateController(_loggerMock.Object, _mediatorMock.Object);

        }
        [Fact(DisplayName = "Update USer ok")]
        public async Task UpdateuserOK()
        {
            //Arrage
            var request = BuildDataUserContextFaker.userUpdateRequest();
            var expectedResponse = Guid.NewGuid();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserUpdateCommand>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.update(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "User Update exception")]
        public async Task UserupdateEx()
        {
            //Arrage
            var request = BuildDataUserContextFaker.userUpdateRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserUpdateCommand>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.update(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        [Fact(DisplayName = "change user status ok")]
        public async Task changestatusOk()
        {
            //Arrage
            var request = BuildDataUserContextFaker.changeUserStatusRequest();
            var expectedResponse = BuildDataUserContextFaker.changeUserStatusResponse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeUserStatusCommand>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.ChangeUserStatus(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "change user status exception")]
        public async Task changestatusok()
        {
            //Arrage
            var request = BuildDataUserContextFaker.changeUserStatusRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeUserStatusCommand>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.ChangeUserStatus(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
    }
}
