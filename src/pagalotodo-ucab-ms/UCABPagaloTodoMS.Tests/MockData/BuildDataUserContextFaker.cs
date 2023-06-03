using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataUserContextFaker
    {
        public static List<AllUserQueryResponse> allUserQueryResponses()
        {
            return new List<AllUserQueryResponse>
            {
                new AllUserQueryResponse
                {
                    Dni = "12345678",
                    Email = "test@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241242050",
                    Username = "Test"
                },
                new AllUserQueryResponse
                {
                    Dni = "22345678",
                    Email = "test2@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241242053",
                    Username = "Tst"
                },
                 new AllUserQueryResponse
                {
                    Dni = "223456578",
                    Email = "test22@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel"
                }

            };
        }
        public static List<OneUserResponse> ListOneUserQueryResponses()
        {
            return new List<OneUserResponse>
            {
                new OneUserResponse
                {
                    Dni = "223456578",
                    Email = "test22@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel",
                    Status = true
                }
            };
        }
        public static OneUserResponse OneUserQueryResponses()
        {
            return
                new OneUserResponse
                {
                    Dni = "223456578",
                    Email = "test22@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel",
                    Status = true
                };
            
        }
        public static UserUpdateRequest userUpdateRequest()
        {
            return new UserUpdateRequest
            {
                Email = "test@example.com",
                Lastname = "Name",
                Name = "Name",
                PhoneNumber = "12345678",
                Username = "Test",
            };
        }
        public static ChangeUserStatusRequest changeUserStatusRequest()
        {
            return new ChangeUserStatusRequest
            {
                Status = true,
                Username = "Test"
            };
        }
        public static ChangeUserStatusResponse changeUserStatusResponse()
        {
            return new ChangeUserStatusResponse
            {
                Status=true,
                Message = "Ok",
                User = "Test"
               
            };
        }
        public static List<AllProvidersResponse> allProvidersResponse()
        {
            return new List<AllProvidersResponse>
            {
                new AllProvidersResponse
                {
                    Dni = "223406578",
                    Email = "test22@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel",
                    CompanyName = "Prueba",
                },
                new AllProvidersResponse
                {
                    Dni = "223459578",
                    Email = "tet22@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel",
                    CompanyName = "Prueba",
                },
                new AllProvidersResponse
                {
                    Dni = "223456778",
                    Email = "test2@example.com",
                    Name = "Name",
                    Lastname = "Name",
                    PhoneNumber = "04241142053",
                    Username = "miguel",
                    CompanyName = "Prueba",
                }
            };
        }
    }
}
