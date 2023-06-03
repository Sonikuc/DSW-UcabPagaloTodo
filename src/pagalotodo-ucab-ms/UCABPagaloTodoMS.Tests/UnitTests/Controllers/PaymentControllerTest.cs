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
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class PaymentControllerTest
    {
        private readonly PaymentController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<PaymentController>> _loggerMock;

        public PaymentControllerTest()
        {
            _loggerMock = new Mock<ILogger<PaymentController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new PaymentController(_loggerMock.Object, _mediatorMock.Object);

        }

        [Fact(DisplayName ="Addservice OK")]
        public async Task addserviceoK()
        {
            //Arrage
            var request = BuildDataPaymentContextFaker.PaymentRequest();
            var expectedResponse = BuildDataPaymentContextFaker.PaymentResponseOK();

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddPaymentCommand>(), default)).ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AddService(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok=result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
            var response = ok.Value as AddPaymentResponse;
            Assert.True(response.success);
            Assert.Equal(expectedResponse.message, response.message);   

        }
        [Fact(DisplayName ="Addservice exception")]
        public async Task AddserviceEx()
        {
            //Arrage
            var request = BuildDataPaymentContextFaker.PaymentRequest();

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddPaymentCommand>(), default)).ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AddService(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}