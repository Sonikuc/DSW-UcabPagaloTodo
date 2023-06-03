using System.Net;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
    public static class AdminMapper
    {
       // Preguntar como hacer esto si tenemos el response sin los mismos atributos que el admin entity
      /*  public static AdminResponse MapEntityAResponse(ValoresEntity entity)
        {
            var response = new ValoresResponse()
            {
                Id = entity.Id,
                Nombre = entity.Nombre + entity.Apellido,
                Identificacion = entity.Identificacion
            };
            return response;
        }*/

        //Metodo que transforma un request de tipo registro de nuevo admin a un AdminEntity
        public static AdminEntity MapRequestToEntity(AdminSignUpRequest request)
        {
            var entity = new AdminEntity()
            {
                Dni = request.Dni,
                Name = request.Name,
                Lastname = request.Lastname,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };
            return entity;
        }
    }
}
