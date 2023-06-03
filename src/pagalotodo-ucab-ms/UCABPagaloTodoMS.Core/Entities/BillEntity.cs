using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
	public class BillEntity : BaseEntity
	{
		public string? ContractNumber { get; set; }
        public string? PhoneNumber { get; set; } 
		public double? Amount { get; set; }
		public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }

        public Guid PaymentOptionId { get; set; }
        public PaymentOptionEntity? PaymentOption { get; set; }
    }
}
