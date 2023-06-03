using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public class BuildDataPaymentContextFaker
    {
        public static AddPaymentRequest PaymentRequest()
        {
            return new AddPaymentRequest
            {
                Amount = 1,
                ContractNumber = "04241242050",
                PaymentOptionId = Guid.NewGuid(),
                PhoneNumber = "1234567890",
                ServiceId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),

            };
        }
        public static AddPaymentResponse PaymentResponseOK()
        {
            return new AddPaymentResponse
            {
                message = "Pago registrado con exito",
                success = true,
            };
        }
        public static AddPaymentOptionRequest PaymentOptionRequestok()
        {
            return new AddPaymentOptionRequest
            {
                ServiceId = Guid.NewGuid(),
                Status = "OK",
                Name = "TEST"
            };
        }
        public static AddPaymentOptionResponse paymentOptionResponseok()
        {
            return new AddPaymentOptionResponse
            {
                success = true,
                message = "Metodo de pago registrado con exito"
            };
        }
        public static List<PaymentOptionsByServiceIdResponse> paymentopServiceIdResponse()
        {
            return new List<PaymentOptionsByServiceIdResponse>
                {
                new PaymentOptionsByServiceIdResponse
                {
                    PaymentOptionId= Guid.NewGuid(),
                    PaymentOptionName = "test",
                    Status= "OK",
                },
                new PaymentOptionsByServiceIdResponse
                {
                    PaymentOptionId= Guid.NewGuid(),
                    PaymentOptionName = "test1",
                    Status= "OK",
                },
                new PaymentOptionsByServiceIdResponse
                {
                    PaymentOptionId= Guid.NewGuid(),
                    PaymentOptionName = "test2",
                    Status= "ok",
                }
            };
        }
    }
}

