using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Mappers
{
	public class ServiceMapper
	{
		//Metodo que transforma un objeto de AddServiceRequest a Service Entity
		public static ServiceEntity MapRequestToEntity(AddServiceRequest request)
		{
			var entity = new ServiceEntity()
			{
				ServiceName = request.ServiceName,
				ContactNumber = request.ContactNumber,
				TypeService = request.TypeService
			};
			return entity;
		}
	}
}

