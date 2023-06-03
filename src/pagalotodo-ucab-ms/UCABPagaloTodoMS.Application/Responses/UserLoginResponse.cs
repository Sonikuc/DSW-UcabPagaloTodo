using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public bool? Success { get; set; }// indica si la conexion fue exitosa o no
        public string? Message { get; set; }// Mensaje de respuesta del servidor
        public string? UserName { get; set; }//obtendra el user de la bd
        public string? Password { get; set; }//obtendra la pass de la bd
        public bool? isAdmin { get; set; }
        public bool? isProvider { get; set; }
        public bool? Status { get; set; }
       
    }
}
