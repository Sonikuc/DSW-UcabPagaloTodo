using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataSignUpContextFaker
    {
        public static UserSignUpRequest userSignUpRequest()
        {
            return new UserSignUpRequest 
            { 
                Dni = "29883898",
                Email = "test@gamil.com",
                Lastname = "Exmaple",
                Name = "Name",
                Password = "Password",
                PhoneNumber = "1234567890",
                Username = "TEST"
            };
        }
        public static UserSignUpResponse UserSignUpResponse()
        {
            return new UserSignUpResponse
            {
                Dni = "29883898",
                Email = "test@gamil.com",
                Lastname = "Exmaple",
                Name = "Name",
                Password = "Password",
                PhoneNumber = "1234567890",
                Username = "TEST"
                
            };
        }
        public static ProviderSignUpRequest ProviderSignUpRequest()
        {
            return new ProviderSignUpRequest
            {
                CompanyName = "Test",
                Dni = "123456789",
                Email = "Provider@gmail.com",
                Lastname = "Test",
                Name = "Test",
                Password = "Password",
                PhoneNumber = "1234567890",
                Username = "Test"
            };
        }
        public static ProviderSignUpResponse ProviderSignUpResponse()
        {
            return new ProviderSignUpResponse
            {
                Dni = "123456789",
                Email = "Provider@gmail.com",
                Lastname = "Test",
                Name = "Test",
                Password = "Password",
                PhoneNumber = "1234567890",
                Username = "Test"
            };
        }
    }
}
