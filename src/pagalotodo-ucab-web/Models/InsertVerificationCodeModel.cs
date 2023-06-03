using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace UCABPagaloTodoWeb.Models
{
    public class InsertVerificationCodeModel
    {
        [NotNull]
        [Required(ErrorMessage = "Codigo de verificacion no valido")]
        [Display(Name = "Codigo de Verificacion")]
        public string? VerificationCode { get; set; }

        [NotNull]
        [Required(ErrorMessage = "Introduzca un email")]
        [EmailAddress(ErrorMessage = "Introduzca un correo v�lido")]
        [Display(Name = "Correo Electronico")]
        public string? Email { get; set; }

    }
}
