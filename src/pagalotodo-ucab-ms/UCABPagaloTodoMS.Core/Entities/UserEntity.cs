using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string? Dni { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public string? VerificationCode { get; set; }// almacena el codigo de verificacion 
        public ICollection<BillEntity>? Bills { get; set; }

    }
}
