using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class CustomerResponse
    {
        
        public string? UserName { get; set; }// token de autenticaciuon que se puede usar en futuras autenticaciones
        public string? Password  { get; set; }
        public string? Email { get; set; }
    }
}
