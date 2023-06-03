using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

using NUnit.Framework.Internal;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Controllers;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Tests.DataSeed;
using UCABPagaloTodoMS.Tests.MockData;
using Xunit;
using Xunit.Sdk;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace UCABPagaloTodoMS.Tests.UnitTests.Controllers
{
    public  class RecoveryPasswordControllerTest
    {
        private readonly RecoveryPasswordController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<RecoveryPasswordController>> _loggerMock;
        private readonly Mock<IUCABPagaloTodoDbContext> _dbContextMock;
        private Mock<IDbContextTransactionProxy> _mockTransaccion;

        public RecoveryPasswordControllerTest()
        {
            _loggerMock = new Mock<ILogger<RecoveryPasswordController>>();
            _mediatorMock = new Mock<IMediator>();
            _dbContextMock = new Mock<IUCABPagaloTodoDbContext>();
            _dbContextMock.Seed();
            _controller = new RecoveryPasswordController(_dbContextMock.Object, _loggerMock.Object, _mediatorMock.Object);
            DataSeed.DataSeed.SetupDbContextData(_dbContextMock);
            _mockTransaccion = new Mock<IDbContextTransactionProxy>();
        }

        [Fact(DisplayName = "RecoveryPasswor - No se encuentra el correo")]
        public async Task Email_Not_Found()
        {
            //Arrange
            var request = BuildDataRecoveryPasswordContextFaker.RecoveryPasswordRequest();
            var expectedResponse = BuildDataRecoveryPasswordContextFaker.SendPasswordResponseNotOK();

            _mediatorMock.Setup(x => x.Send(It.IsAny<SendVerificationCodeCommand>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Post(request);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundRequestResult = (NotFoundObjectResult)result;
            Assert.Equal("La dirección de correo electrónico no está registrada", notFoundRequestResult.Value);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequestResult.StatusCode);
        }
        [Fact(DisplayName = "RecoveryPasswor - excepcion no controlado")]
        public async Task Exception()
        {
            //Arrage
            var request = new RecoveryPasswordRequest { Email = " test@gmail.com" };
            var command = new SendVerificationCodeCommand { Email = "test@gmail.com" , VerificationCode = "123456" };
            _mediatorMock.Setup(m => m.Send(command, default)).ThrowsAsync(new Exception("Excepción no controlada"));
            
            // Act
            var result = await _controller.Post(request);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }
        [Fact(DisplayName = "RecoveyPassword exception")]
        public async Task PostEx()
        {
            
            var request = new RecoveryPasswordRequest { Email = "sedetcontrerasc@gmail.com" };
            var expectedUser = new UserEntity { Email = request.Email };
            var expectedResponse = BuildDataRecoveryPasswordContextFaker.SendPasswordResponseOK();
            var dbContext = _dbContextMock.Object;

            _mediatorMock.Setup(m => m.Send(It.IsAny<SendVerificationCodeCommand>(), default))
                .ThrowsAsync(new Exception());
            var mockTransaction = new Mock<IDbContextTransactionProxy>();
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);
            // Act
            var result = await _controller.Post(request);


            // Assert

            Assert.IsType<ObjectResult>(result); // Verificar que la respuesta es del tipo OkObjectResult
            var okResult = result as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, okResult.StatusCode); // Verificar que el código de estado es 500 InternalServerError

        }

        [Fact(DisplayName = "RecoveryPassword - result ok")]
        public async Task ReturnOK()
        {
            // Arrange
            var request = new RecoveryPasswordRequest { Email = "sedetcontrerasc@gmail.com" };
            var expectedUser = new UserEntity { Email = request.Email };
            var expectedResponse = BuildDataRecoveryPasswordContextFaker.SendPasswordResponseOK();
            var dbContext = _dbContextMock.Object;

            _mediatorMock.Setup(m => m.Send(It.IsAny<SendVerificationCodeCommand>(), default))
                .ReturnsAsync(expectedResponse);
            var mockTransaction = new Mock<IDbContextTransactionProxy>();
            _dbContextMock.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);
            // Act
            var result = await _controller.Post(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult statusCodeResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, statusCodeResult.StatusCode);

            var response = Assert.IsType<SendPasswordResponse>(statusCodeResult.Value);
            Assert.Equal(expectedResponse.Send, response.Send);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact(DisplayName = "VerifyCode - verificacion ok")]
        public async Task verifyOK()
        {
            // Arrage
            var request = new InsertVerificationCodeRequest { Email = "sedetcontrerasc@gmail.com", VerificationCode = "762729" };
            var expectedresponse = BuildDataRecoveryPasswordContextFaker.verifycoderesponseOK();
            var query = new VerifyRecoveryCodeQuery(request);

            _mediatorMock.Setup(x => x.Send(It.IsAny<VerifyRecoveryCodeQuery>(), default)).ReturnsAsync( expectedresponse);
            // Act
            var result = await _controller.Verify(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            // Verifica si `Value` es nulo
            OkObjectResult okObject = result as OkObjectResult;
            Assert.NotNull(okObject.Value);
            Assert.Equal(StatusCodes.Status200OK, okObject.StatusCode);

            // Accede a las propiedades de la respuesta
            var response = okObject.Value as RecoveryPasswordResponse ;
            Assert.NotNull(response);
            Assert.Equal(expectedresponse.Message, response.Message);
            Assert.Equal(expectedresponse.Veryfy, response.Veryfy);
        }
    

        [Fact(DisplayName = "VerifyCode - verificacion no ok")]
        public async Task verifynook()
        {
            // Arrage
            var request = new InsertVerificationCodeRequest { Email = "sedetcontrerasc@gmail.com", VerificationCode = "76279" };
            var expectedresponse = BuildDataRecoveryPasswordContextFaker.verifycoderesponseNoOK();

            _mediatorMock.Setup(x => x.Send(It.IsAny<VerifyRecoveryCodeQuery>(), default(CancellationToken))).ReturnsAsync(expectedresponse);
            // Act
            var result = await _controller.Verify(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            // Verifica si `Value` es nulo
            OkObjectResult onfObject = result as OkObjectResult;
            Assert.NotNull(onfObject.Value);
            Assert.Equal(StatusCodes.Status200OK, onfObject.StatusCode);

            // Accede a las propiedades de la respuesta
            var response = onfObject.Value as RecoveryPasswordResponse;
            Assert.NotNull(response);
            Assert.Equal(expectedresponse.Message, response.Message);
            Assert.Equal(expectedresponse.Veryfy, response.Veryfy);
        }

        [Fact(DisplayName ="VerifyCode - request null" )]
        public async Task Verify_requestNull()
        {
            // Arrange
            InsertVerificationCodeRequest request = null;

            // Act
            var result = await _controller.Verify(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("La solicitud no puede ser nula", badRequestResult.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
        [Fact(DisplayName = "Verify - excepción no controlada")]
        public async Task Verify_Exception()
        {
            // Arrange
            var request = new InsertVerificationCodeRequest { Email = "sedetcontrerasc@gmail.com", VerificationCode = "76279" };
            var query = new VerifyRecoveryCodeQuery(request);
            var dbContext = _dbContextMock.Object;

            // Configura el objeto _mediatorMock para que genere una excepción no controlada
            _mediatorMock.Setup(m => m.Send(It.IsAny<VerifyRecoveryCodeQuery>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Verify(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
        [Fact(DisplayName = "Verify - User no encontrado")]
        public async Task VerifyUsernofound()
        {
            // Arrange
            var request = new InsertVerificationCodeRequest { Email = "sedetcontreras@gmail.com", VerificationCode = "76279" };
            var query = new VerifyRecoveryCodeQuery(request);
           // var dbContext = _dbContextMock.Object;

            // Configura el objeto _mediatorMock para que genere una excepción no controlada
            _mediatorMock.Setup(m => m.Send(It.IsAny<VerifyRecoveryCodeQuery>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Verify(request);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }
        [Fact(DisplayName = "Verify - ArgumentNUllEx")]
        public async Task VerifyArgumentNUllEx()
        {
            // Arrange
            var request = new InsertVerificationCodeRequest { Email = "sedetcontrerasc@gmail.com", VerificationCode = null };
            var query = new VerifyRecoveryCodeQuery(request);
            // var dbContext = _dbContextMock.Object;

            // Configura el objeto _mediatorMock para que genere una excepción no controlada
            _mediatorMock.Setup(m => m.Send(It.IsAny<VerifyRecoveryCodeQuery>(), default)).ThrowsAsync(new ArgumentNullException());

            // Act
            var result = await _controller.Verify(request);

            // Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, statusCodeResult.StatusCode);
        }
        //// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [Fact(DisplayName = "ChangePassword - ok")]
        public async Task changepasswordOK()
        {
            // Arrange
            var request = BuildDataRecoveryPasswordContextFaker.ChangePasswordRequest();
            var expectedResponse = BuildDataRecoveryPasswordContextFaker.verifycoderesponseOK();// { Success = true };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.ChangePassword(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var response = okResult.Value as RecoveryPasswordResponse;
            Assert.NotNull(response); // Verificar que la variable response no es nula

            Assert.Equal(expectedResponse.Veryfy, response.Veryfy);
        }
        [Fact(DisplayName = "ChangePassword - nullexception")]
        public async Task changepasswordnull()
        {
            {
                // Arrange
                var request = BuildDataRecoveryPasswordContextFaker.ChangePasswordRequest();
                var command = new ChangePasswordCommand(request);
                _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default)).ThrowsAsync(new ArgumentNullException());

                // Act
                var result = await _controller.ChangePassword(request);

                // Assert

                Assert.IsType<BadRequestObjectResult>(result); // Verificar que la respuesta es del tipo OkObjectResult
                var okResult = result as BadRequestObjectResult;
                Assert.Equal(StatusCodes.Status400BadRequest, okResult.StatusCode); // Verificar que el código de estado es 500 InternalServerError

            }
        }
        [Fact(DisplayName = "ChangePassword - Exception")]
        public async Task changepasswordEx()
        {
            {
                // Arrange
                var request = BuildDataRecoveryPasswordContextFaker.ChangePasswordRequest();
                var command = new ChangePasswordCommand(request);
                _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default)).ThrowsAsync(new Exception());

                // Act
                var result = await _controller.ChangePassword(request);

                // Assert

                Assert.IsType<ObjectResult>(result); // Verificar que la respuesta es del tipo OkObjectResult
                var okResult = result as ObjectResult;
                Assert.Equal(StatusCodes.Status500InternalServerError, okResult.StatusCode); // Verificar que el código de estado es 500 InternalServerError

               
            }
        }
    }

    
}
