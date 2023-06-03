using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class UpdateUserViewModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de teléfono debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de teléfono debe estar entre 11 y 15 caracteres.")]
        public string PhoneNumber { get; set; }

        public UpdateUserViewModel()
        {
            Email = "";
            UserName = "";
            PhoneNumber = "";
            Name = "";
            Lastname = "";
        }
    }
}
