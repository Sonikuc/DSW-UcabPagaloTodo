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
    public class UserQueryControllerTest
    {

        private readonly UserQueryController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UserQueryController>> _loggerMock;

        public UserQueryControllerTest()
        {
            _loggerMock = new Mock<ILogger<UserQueryController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserQueryController(_loggerMock.Object, _mediatorMock.Object);

        }
        [Fact(DisplayName = "Allusers ok")]
        public async Task AllUsersOk()
        {
            //Arrage
            var expectedResponse = BuildDataUserContextFaker.allUserQueryResponses();

            _mediatorMock.Setup(x => x.Send(It.IsAny<AllUserQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AllUsers();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "AllUsers exception")]
        public async Task AllUsersEx()
        {
            //Arrage
            _mediatorMock.Setup(x => x.Send(It.IsAny<AllUserQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AllUsers();

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        //////////////////////////////////////////////////////////////////////////////
        [Fact(DisplayName = "User By UserName ok")]
        public async Task UserbyusernameOK()
        {
            //Arrage
            var request = "Miguel";
            var expectedResponse = BuildDataUserContextFaker.OneUserQueryResponses();
            _mediatorMock.Setup(m => m.Send(It.IsAny<UserByUsernameQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byUsername(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
            
        }
        [Fact(DisplayName = "User By UserName exception")]
        public async Task byservisenameEx()
        {
            //Arrage
            var request = "Miguel";

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserByUsernameQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byUsername(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }
        [Fact(DisplayName = "ByDni ok")]
        public async Task ByDniok()
        {
            //Arrage
            var request = "29883898";
            var expectedResponse = BuildDataUserContextFaker.ListOneUserQueryResponses();

            _mediatorMock.Setup(x => x.Send(It.IsAny<UserByDNIQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.byDni(request);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "ByDni exception")]
        public async Task ByDniEx()
        {
            //Arrage
            var request = "29883898";
            ;
            _mediatorMock.Setup(x => x.Send(It.IsAny<UserByDNIQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.byDni(request);

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }

        [Fact(DisplayName = "AllProvider ok")]
        public async Task AllProviderOK()
        {
            //Arrage
            
            var expectedResponse = BuildDataUserContextFaker.allProvidersResponse();

            _mediatorMock.Setup(x => x.Send(It.IsAny<AllProvidersQuery>(), default(CancellationToken)))
                             .ReturnsAsync(expectedResponse);

            //Act
            var result = await _controller.AllProviders();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        }
        [Fact(DisplayName = "AllProvider exception")]
        public async Task AllProviderEx()
        {
            //Arrage
           
            _mediatorMock.Setup(x => x.Send(It.IsAny<AllProvidersQuery>(), default(CancellationToken)))
                             .ThrowsAsync(new Exception());

            //Act
            Func<Task> result = async () => await _controller.AllProviders();

            // Assert
            await Assert.ThrowsAsync<Exception>(result);

        }

    }
}
