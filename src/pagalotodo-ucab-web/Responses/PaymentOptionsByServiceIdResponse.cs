using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class PaymentOptionsByServiceIdResponse
    {
        public Guid PaymentOptionId { get; set; }
        public string? PaymentOptionName { get; set; }
        public string? Status { get; set; }
    }
}
