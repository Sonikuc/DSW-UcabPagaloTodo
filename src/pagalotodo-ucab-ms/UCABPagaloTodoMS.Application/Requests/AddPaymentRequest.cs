using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class AddPaymentRequest
    {
        public string? ContractNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Amount { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid PaymentOptionId { get; set; }
    }
}
