using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class AllBillsQueryResponse
    {
        public string? ContractNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; } //usuario que pago
        public Guid ServiceId { get; set; }
        public string? ServiceName { get; set; }


    }
}
