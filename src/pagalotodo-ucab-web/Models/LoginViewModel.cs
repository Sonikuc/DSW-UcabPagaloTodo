using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace UCABPagaloTodoWeb.Models
{
    public class LoginViewModel
    {
        [NotNull]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [NotNull]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos... 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public LoginViewModel()
        {
            Username = "";
            Password = "";
        }
    }
}
