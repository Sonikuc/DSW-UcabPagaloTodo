using MockQueryable.Moq;
using Moq;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Tests.DataSeed
{
    public static class DataSeed
    {
        public static void SetupDbContextData(this Mock<IUCABPagaloTodoDbContext> mockContext)
        {
            var usuario = new UserEntity
            {
                Id = new Guid("762d9cb4-66aa-44c3-9d1f-4fe473c51cc5"),
                Name = "Sedet",
                Lastname = "Contreras",
                Dni = "29883898",
                Email = "sedetcontrerasc@gmail.com",
                Username = "SedetC",
                Password = "se170311",
            };
            var users = new List<UserEntity> { usuario };
            mockContext.Setup(c => c.UserEntities).Returns(users.AsQueryable().BuildMockDbSet().Object); 
        }
            
    }
}