using System.Net;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class UserMapper
    {   //Metodo que transforma un objeto de UserEntity a UserSignUpResponse
         public static UserSignUpResponse MapEntityToResponse(UserEntity request)
          {
              var response = new UserSignUpResponse()
              {
                  Dni = request.Dni,
                  Name = request.Name,
                  Lastname = request.Lastname,
                  Username = request.Username,
                  Email = request.Email,
                  Password = request.Password,
                  PhoneNumber = request.PhoneNumber
              };
              return response;
          }
        //Metodo que transforma un objeto de ProviderEntity a ProviderSignUpResponse
        public static ProviderSignUpResponse MapEntityToCustomerResponse(UserEntity request)
        {
            var response = new ProviderSignUpResponse()
            {
                Dni = request.Dni,
                Name = request.Name,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };
            return response;
        }

        //Metodo que transforma un request UserSignUpRequest a un UserEntity
        public static UserEntity MapRequestToEntity(UserSignUpRequest request)
        {
            var entity = new UserEntity()
            {
                Dni = request.Dni,
                Name = request.Name,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Status = true
            };
            return entity;
        }

        //Metodo que transforma un request de tipo registro de nuevo admin a un ProviderEntity
        public static ProviderEntity MapRequestToProviderEntity(ProviderSignUpRequest request)
        {
            var entity = new ProviderEntity()
            {
                Dni = request.Dni,
                Name = request.Name,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                Status = true

            };
            return entity;
        }

        public static ProviderEntity MapUserEntityToProviderEntity(ProviderEntity request)
        {
            var entity = new ProviderEntity()
            {
                Dni = request.Dni,
                Name = request.Name,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                Status = true

            };
            return entity;
        }


    }
}
