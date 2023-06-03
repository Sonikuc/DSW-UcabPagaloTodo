using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace UCABPagaloTodoWeb.Models
{
    public class NewPasswordModel
    {
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        [MinLength(6, ErrorMessage = "La longitud mínima de la contraseña es de 6 caracteres.")]
        [PasswordPropertyText]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El campo Confirmar contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [PasswordPropertyText]
        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }

    }
}

