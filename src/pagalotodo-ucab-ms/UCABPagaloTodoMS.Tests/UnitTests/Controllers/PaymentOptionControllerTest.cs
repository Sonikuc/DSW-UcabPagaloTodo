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
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class PaymentOptionControllerTest
    {
        private readonly PaymentOptionController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<PaymentOptionController>> _loggerMock;

        public PaymentOptionControllerTest()
        {
            _loggerMock = new Mock<ILogger<PaymentOptionController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new PaymentOptionController(_loggerMock.Object, _mediatorMock.Object);

        }

        [Fact(DisplayName = "AddPaymentOption OK")]
        public async Task addpaymentoptionOK()
        {
            //Arrage
            var request = BuildDataPaymentContextFaker.PaymentOptionRequestok();
            var expectedResponse = BuildDataPaymentContextFaker.paymentOptionResponseok();

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddPaymentOptionCommand>(), default)).ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AddPaymentOption(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
            var response = ok.Value as AddPaymentOptionResponse;
            Assert.True(response.success);
            Assert.Equal(expectedResponse.message, response.message);

        }
        [Fact(DisplayName = "AddPaymentOption exception")]
        public async Task addpaymentoptionex()
        {
            //Arrage
            var request = BuildDataPaymentContextFaker.PaymentOptionRequestok();

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddPaymentOptionCommand>(), default)).ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AddPaymentOption(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
        [Fact (DisplayName ="Payment Option service Id")]
        public async Task paymentoptionsericeid()
        {
            var request = Guid.NewGuid();
            var expectedResponse = BuildDataPaymentContextFaker.paymentopServiceIdResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<PaymentOptionsByServiceIdQuery>(), default)).ReturnsAsync(expectedResponse);

            var result = await _controller.PaymentOptionsByServiceId(request);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        }
        [Fact(DisplayName = " Payment Option service Id Exception")]
        public async Task paymentoptionsericeidEx()
        {
            var request = Guid.NewGuid();
            var expectedResponse = BuildDataPaymentContextFaker.paymentopServiceIdResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<PaymentOptionsByServiceIdQuery>(), default)).ThrowsAsync(new Exception());

            Func<Task> result = async () => await _controller.PaymentOptionsByServiceId(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);
        }
    }
}
