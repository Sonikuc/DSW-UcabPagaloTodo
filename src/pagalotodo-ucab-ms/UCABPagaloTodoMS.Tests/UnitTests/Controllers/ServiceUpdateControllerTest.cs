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
    public class ServiceUpdateControllerTest
    {
        private readonly ServiceUpdateController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ServiceUpdateController>> _loggerMock;

        public ServiceUpdateControllerTest()
        {
            _loggerMock = new Mock<ILogger<ServiceUpdateController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new ServiceUpdateController(_loggerMock.Object, _mediatorMock.Object);

        }
        [Fact(DisplayName = "ServiceUpdate ok")]
        public async Task AllServiceOK()
        {
            //Arrage
            var request = BuildDataServicesContextFaker.serviceUpdateRequest();
            var expectedResponse = BuildDataServicesContextFaker.serviceUpdateResponse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ServiceUpdateCommand>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.update(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "ServiceUpdate exception")]
        public async Task ServiceUpdateEx()
        {
            //Arrage
            var request = BuildDataServicesContextFaker.serviceUpdateRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ServiceUpdateCommand>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.update(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
    }
}
