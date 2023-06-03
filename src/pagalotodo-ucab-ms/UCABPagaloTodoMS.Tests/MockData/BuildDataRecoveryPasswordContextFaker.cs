using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataRecoveryPasswordContextFaker
    {
        public static Faker<RecoveryPasswordRequest> RecoveryPasswordRequest()
        {
            Randomizer seed = new Randomizer(100);
            return new Faker<RecoveryPasswordRequest>()
                .RuleFor(cs => cs.Email, fk => fk.Internet.Email());
        }
        public static SendPasswordResponse SendPasswordResponseOK()
        {
            return new SendPasswordResponse
            {
                Message = "Se envio a su correo un mensaje con su codigo de verificacion",
                Send = true
            };
        }
        public static SendPasswordResponse SendPasswordResponseNotOK()
        {
            return new SendPasswordResponse
            {
                Message = "Email nulo, no es posible enviar codigo de verificacion",
                Send = false
            };
        }

        public static InsertVerificationCodeRequest verifycoderequest()
        {
            return new InsertVerificationCodeRequest
            {
               Email = "verificacion@gmail.com",
               VerificationCode = "123456"
            };
        }
    
        public static RecoveryPasswordResponse verifycoderesponseOK()
        {
            return new RecoveryPasswordResponse
            {
                Veryfy = true,
                Message = "Verificacion de codigo de recuperacion exitosa"
            };
        }
        public static RecoveryPasswordResponse verifycoderesponseNoOK()
        {
            return new RecoveryPasswordResponse
            {
                Veryfy = false,
                Message = "Verificacion de codigo de recuperacion fallido"
            };
        }
        public static ChangePasswordRequest ChangePasswordRequest()
        {
            return new ChangePasswordRequest
            {
                Email = "test@gmail.com",
                Password = "123455",
                VerifyPassword = "123455"
            };
        }
       
    }
}
