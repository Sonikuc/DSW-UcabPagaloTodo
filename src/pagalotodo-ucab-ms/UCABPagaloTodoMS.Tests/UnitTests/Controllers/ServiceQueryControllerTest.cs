using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using Xunit.Sdk;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class ServiceQueryControllerTest
    {
        private readonly ServiceQueryController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ServiceQueryController>> _loggerMock;

        public ServiceQueryControllerTest()
        {
            _loggerMock = new Mock<ILogger<ServiceQueryController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new ServiceQueryController(_loggerMock.Object, _mediatorMock.Object);

        }
        [Fact(DisplayName = "AllServices ok")]
        public async Task AllServiceOK()
        {
            //Arrage
            var expectedResponse = BuildDataServicesContextFaker.AllServicesQueryResponse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<AllServicesQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AllServices();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "AllServices exception")]
        public async Task AllserviceEx()
        {
            //Arrage
            _mediatorMock.Setup(x => x.Send(It.IsAny<AllServicesQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AllServices();

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        //////////////////////////////////////////////////////////////////////////////
        [Fact(DisplayName = "ByServiceName ok")]
        public async Task ByServiceNameOK()
        {
            //Arrage
            var request = BuildDataServicesContextFaker.OneServiceRequest();
            var expectedResponse = BuildDataServicesContextFaker.OneServiceResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ServiceByServiceNameQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byServiceName(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "ByServiceName queryNull")]
        public async Task ByServiceNamequerynull()
        {
            //Arrage
            var request = BuildDataServicesContextFaker.OneServiceRequest();
            var result = await _controller.byServiceName(request) ;

             // Act and Assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<ServiceByServiceNameQuery>(), default), Times.Once);
        }
        [Fact(DisplayName = "byServiceName exception")]
        public async Task byservisenameEx()
        {
            //Arrage
            var request = BuildDataServicesContextFaker.OneServiceRequest();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ServiceByServiceNameQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byServiceName(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        [Fact(DisplayName = "ByGuid ok")]
        public async Task ByGuidok()
        {
            //Arrage
            var request = Guid.NewGuid();
            var expectedResponse = BuildDataServicesContextFaker.OneServiceResponse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<ServiceByGuidQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byGuid(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "ByGuid exception")]
        public async Task ByGuidEx()
        {
            //Arrage
            var request = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<ServiceByGuidQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byGuid(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
    }
}
