using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Infrastructure.Migrations;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class RecoveryPasswordResponse
    {
        public string? Message { get; set; }
        public bool? Veryfy { get; set; }
    }
}
