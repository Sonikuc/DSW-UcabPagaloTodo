using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;


namespace UCABPagaloTodoMS.Tests.DataSeed
{
    public static class UserEntityDataSeed
    {

        public static void Seed(this Mock<IUCABPagaloTodoDbContext> mockContext)
        {
            var users = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Dni = "12345678",
                    Name = "John",
                    Lastname = "Doe",
                    Username = "johndoe",
                    Email = "johndoe@example.com",
                    Password = "password123",
                    PhoneNumber = "555-1234",
                    Status = true,
                    VerificationCode = "123453",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Dni = "87654321",
                    Name = "Jane",
                    Lastname = "Doe",
                    Username = "janedoe",
                    Email = "janedoe@example.com",
                    Password = "password456",
                    PhoneNumber = "555-5678",
                    Status = true,
                    VerificationCode = "654321",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Dni = "29883898",
                    Name = "Sedet",
                    Lastname = "Contreras",
                    Username = "SedetC",
                    Email = "sedetcontrerasc@gmail.com",
                    Password = "se170311",
                    PhoneNumber = "555-1234",
                    Status = true,
                    VerificationCode = "321456",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            // Convertir la lista de usuarios en un objeto IQueryable para poder utilizar MockQueryable
            var usersQueryable = users.AsQueryable();
           

            // Configurar el DbSet de la entidad UserEntity en el contexto de base de datos falso
            mockContext.Setup(c => c.UserEntities).Returns(usersQueryable.BuildMockDbSet().Object);
        }
    }
}