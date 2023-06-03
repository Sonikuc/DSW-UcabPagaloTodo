using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using UCABPagaloTodoMS.Application.Queries;
using static UCABPagaloTodoMS.Tests.DataSeed.DataSeed;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Core.Entities;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Amqp.Transaction;
using System;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class AddservicesTest
    {
        private readonly AddServiceController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<AddServiceController>> _loggerMock;

        public AddservicesTest()
        {
            _loggerMock = new Mock<ILogger<AddServiceController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new AddServiceController(_loggerMock.Object, _mediatorMock.Object);

        }

        [Fact(DisplayName ="AddService ok resturn 200")]
        public async Task AddServiceOK()
        {
            // Arrange
            var request = BuildDataContextFaker.addServiceRequest();
            var expectedresponse = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddServiceCommand>(), default(CancellationToken)))
             .ReturnsAsync(expectedresponse);

            // Act
            ActionResult result = await _controller.AddService(request);
            OkObjectResult okObject = result as OkObjectResult;

            // Assert      
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);
            var addServiceResponse = Assert.IsType<AddServiceResponse>(okObject.Value);
           
           
        }
        [Fact]
        public async Task AddService_throwException()
        {
            // Arrange
            var request = new AddServiceRequest
            {
                UserName = "TST",
                ContactNumber = "10000100000 ",
                ServiceName = "TEST SERV"
            };
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddServiceCommand>(), default)).ThrowsAsync(new Exception());

            // Act
            Func<Task> result = async () => await _controller.AddService(request);


            // Assert
            await Assert.ThrowsAsync<Exception>(result);
           
        }

    }
}