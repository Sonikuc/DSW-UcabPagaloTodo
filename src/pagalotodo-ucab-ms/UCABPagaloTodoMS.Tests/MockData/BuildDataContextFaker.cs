
using Bogus;
using MassTransit.Futures.Contracts;
using System.Security.Cryptography.X509Certificates;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Tests.MockData
{


    public static class BuildDataContextFaker
    {
        public static Faker<UserEntity> BuildUserEntity()
        {
            Randomizer.Seed = new Random();
            return new Faker<UserEntity>()
                .RuleFor(cs => cs.Id, fk => Guid.NewGuid())
                .RuleFor(cs => cs.Username, fk => fk.Person.UserName)
                .RuleFor(cs => cs.Password, fk => fk.Lorem.Word());

        }
        public static UserLoginResponse adminLoginResponse()
        {
            var data = new UserLoginResponse
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Success = true,
                Message = "Inicio de sesión exitoso",
                isAdmin = true,
                Status = true

            };
            return data;
        }
        public static UserLoginResponse userLoginResponseStatusTrue()
        {
            var data = new UserLoginResponse
            {
                Id = Guid.NewGuid(),
                UserName = "user",
                Success = true,
                Message = "Inicio de sesión exitoso",
                isAdmin = false,
                Status = true

            };
            return data;
        }
        public static UserLoginResponse userLoginResponseStatusFalse()
        {
            var data = new UserLoginResponse
            {
                Id = Guid.NewGuid(),
                UserName = "user",
                Success = true,
                Message = "Inicio de sesión exitoso",
                isAdmin = false,
                Status = false

            };
            return data;
        }
        public static AddServiceRequest addServiceRequest()
        {
            var data = new AddServiceRequest
            {
                UserName = "TEST",
                ContactNumber = "10000000000 ",
                ServiceName = "TEST SERVICE"
            };
            return data;
        }
        public static ServiceEntity serviceEntity()
        {
            var data = new ServiceEntity
            {
                ServiceName = "TEST SERVICE",
                ContactNumber = "10000000000",
                Id = new Guid("762d9cc3-66aa-44c3-9d1f-4fe473c51cc5"),
                ProviderId = new Guid("762d9cc3 - 66aa - 44c2 - 9d1f - 4fe473c51cc5"),
                CreatedAt = DateTime.Now,
                CreatedBy = "admin",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "admin",


            };
            return data;
        }
        public static AddServiceResponse addServiceResponse()
        {
            var data = new AddServiceResponse
            {
                success = true,
                message = "Servico agragado con exito"
            };
            return data;
        }
    }
}

