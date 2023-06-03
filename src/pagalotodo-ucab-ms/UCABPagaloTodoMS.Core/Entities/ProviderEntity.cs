using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class ProviderEntity : UserEntity
    {
        public string? CompanyName { get; set; }
        public ICollection<ServiceEntity>? Service { get; set;}
    }
}
