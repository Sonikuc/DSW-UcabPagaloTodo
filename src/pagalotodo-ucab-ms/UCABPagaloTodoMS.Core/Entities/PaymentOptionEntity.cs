using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
	public class PaymentOptionEntity : BaseEntity
	{
		public string? Name { get; set; }
        
        public string? Status { get; set; }

        public Guid ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }
    }
}
