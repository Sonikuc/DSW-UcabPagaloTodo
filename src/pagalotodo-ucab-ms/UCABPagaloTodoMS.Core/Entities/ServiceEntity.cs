using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
	public class ServiceEntity : BaseEntity
	{
		public string? ServiceName { get; set; }
        public string? TypeService { get; set; }
        public string? ContactNumber { get; set; }
		public Guid ProviderId { get; set; }
		public ProviderEntity? Provider { get; set; }
        public ICollection<BillEntity>? Bills { get; set; }
        public ICollection<PaymentOptionEntity>? PaymentValidation { get; set; }
    }
}
