using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataServicesContextFaker
    {
        public static ServiceDeleteRequest ServiceDeleteRequest()
        {
            return new ServiceDeleteRequest
            {
                ServiceName="test",
                UserName="test",
            };
        }
        public static ServiceDeleteResponse serviceDeleteResponse()
        {
            return new ServiceDeleteResponse { Message = "test" };
        }
        public static List<AllServicesQueryResponse> AllServicesQueryResponse()
        {
            return new List<AllServicesQueryResponse>
            {
                new AllServicesQueryResponse
                {
                    CompanyName = "test",
                    ServiceId = Guid.NewGuid(),
                    ContactNumber = "04241242050",
                    ServiceName = "test",
                    TyperService = "example",
                    ProviderUsername = "test"
                },
                new AllServicesQueryResponse
                {
                    CompanyName = "test2",
                    ServiceId = Guid.NewGuid(),
                    ContactNumber = "04241252050",
                    ServiceName = "test",
                    TyperService = "example",
                    ProviderUsername = "test"
                },
                new AllServicesQueryResponse
                {
                    CompanyName = "test3",
                    ServiceId = Guid.NewGuid(),
                    ContactNumber = "04241342050",
                    ServiceName = "test",
                    TyperService = "example",
                    ProviderUsername = "test"
                }
            };
           
        }
           
        
        public static OneServiceRequest OneServiceRequest()
        {
            return new OneServiceRequest
            {
                ProviderUsername = "test",
                ServiceName = "test"
            };
        }
        public static OneServiceResponse OneServiceResponse()
        {
            return new OneServiceResponse
            {
                CompanyName = "test",
                ServiceId = Guid.NewGuid(),
                ContactNumber = "04241242050",
                ServiceName = "test",
                TyperService = "example",
                ProviderUsername = "test",

            };
        }
        public static ServiceUpdateRequest serviceUpdateRequest()
        {
            return new ServiceUpdateRequest
            {
                ServiceName = "test",
                TypeService = "Example",
                ContactNumber = "04241242050",
                UserName = "test"

            };
        }
        public static ServiceUpdateResponse serviceUpdateResponse()
        {
            return new ServiceUpdateResponse
            {
                CompanyName = "test",
                ContactNumber = "04241242050",
                ProviderUsername = "PrroviderTest",
                ServiceName = "test",
                TyperService = "ëxample"
            };
        }
    }
}
