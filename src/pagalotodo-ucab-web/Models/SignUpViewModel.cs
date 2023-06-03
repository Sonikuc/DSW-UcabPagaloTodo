using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class SignUpViewModel
    {
		[Required(ErrorMessage = "Introduzca su cedula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de Cédula debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de Cédula debe estar entre 7 y 8 caracteres.")]
        public string DNI { get; set; }

		[Required(ErrorMessage = "Introduzca su nombre")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Introduzca su apellido")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Introduzca su nombre de usuario")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repita la contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmationpassword { get; set; }

        [Required(ErrorMessage = "Introduzca su número de telefono")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de teléfono debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de teléfono debe estar entre 11 y 15 caracteres.")]
        public string PhoneNumber { get; set; }

        public SignUpViewModel()
        {
            Email = "";
            Password = "";
            confirmationpassword = "";
            Name = "";
            DNI = "";
            LastName = "";
            UserName = "";
            PhoneNumber = "";
		}

    }
}