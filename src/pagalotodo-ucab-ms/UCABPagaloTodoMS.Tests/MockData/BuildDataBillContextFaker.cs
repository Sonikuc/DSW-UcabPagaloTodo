using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Tests.MockData
{
    public static class BuildDataBillContextFaker
    {
        public static List<AllBillsQueryResponse> AllBillsQueryResponses()
        {
            return new List<AllBillsQueryResponse>
            {
                new AllBillsQueryResponse
                {
                    Amount = 1,
                    ContractNumber="123456789",
                    Date= DateTime.Now,
                    PhoneNumber="987654321",
                    ServiceId=Guid.NewGuid(),
                    ServiceName = "test",
                    UserId=Guid.NewGuid(),
                    UserName="test"

                },
                new AllBillsQueryResponse
                {
                    Amount = 100,
                    ContractNumber="123453789",
                    Date= DateTime.Now,
                    PhoneNumber="987634321",
                    ServiceId=Guid.NewGuid(),
                    ServiceName = "test",
                    UserId=Guid.NewGuid(),
                    UserName="test"

                }
            };
        }
    }
}
