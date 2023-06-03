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
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;

namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public class BillQueryControllerTest
    {
        private readonly BillQueryController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<BillQueryController>> _loggerMock;

        public BillQueryControllerTest()
        {
            _loggerMock = new Mock<ILogger<BillQueryController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new BillQueryController(_loggerMock.Object, _mediatorMock.Object);

        }
        [Fact(DisplayName = "AllBills ok")]
        public async Task AllBills()
        {
            //Arrage
            var expectedResponse = BuildDataBillContextFaker.AllBillsQueryResponses();

            _mediatorMock.Setup(x => x.Send(It.IsAny<AllBillsQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AllBills();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "Allbills exception")]
        public async Task AllbillsEx()
        {
            //Arrage
            _mediatorMock.Setup(x => x.Send(It.IsAny<AllBillsQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AllBills();

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        [Fact(DisplayName = "Bill ByUserID ok")]
        public async Task ByUserIdOK()
        {
            //Arrage
            var request = Guid.NewGuid();
            var expectedResponse = BuildDataBillContextFaker.AllBillsQueryResponses();

            _mediatorMock.Setup(x => x.Send(It.IsAny<BillByUserIdQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byUserId(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "Bill ByUserID exception")]
        public async Task ByUserIDEX()
        {
            //Arrage
            var request = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<BillByUserIdQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byUserId(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        [Fact(DisplayName = "Bill ByServiceID ok")]
        public async Task ByServiceIDOK()
        {
            //Arrage
            var request = Guid.NewGuid();
            var expectedResponse = BuildDataBillContextFaker.AllBillsQueryResponses();

            _mediatorMock.Setup(x => x.Send(It.IsAny<BillByServiceIdQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byServiceId(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "Bill ByServiceID exception")]
        public async Task ByServiceIDEX()
        {
            //Arrage
            var request = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<BillByServiceIdQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byServiceId(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
    }
}
