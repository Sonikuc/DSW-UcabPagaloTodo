using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class SignUpProviderViewModel
    {

        [Required(ErrorMessage = "Introduzca su cedula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número cédula debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de cedula debe estar entre 7 y 8 caracteres.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Introduzca su nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Introduzca su apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Introduzca su nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduzca la contraseña")]
        [MinLength(8, ErrorMessage = "La contrase�a debe tener minimo 8 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repita la contrase�a")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmationpassword { get; set; }

        [Required(ErrorMessage = "Introduzca su numero de telefono")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre de la compañia")]
        public string CompanyName { get; set; }

        public SignUpProviderViewModel()
        {
            Email = "";
            Password = "";
            confirmationpassword = "";
            Name = "";
            DNI = "";
            LastName = "";
            UserName = "";
            PhoneNumber = "";
            CompanyName = "";
        }

    }
}
