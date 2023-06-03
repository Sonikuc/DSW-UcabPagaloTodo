using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class AddServiceViewModel
    {
        [Required(ErrorMessage = "Introduzca el nombre del servicio")]
        public string? ServiceName { get; set; }

        [Required(ErrorMessage = "Introduzca el tipo de servicio")]
        public string? TypeService { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de teléfono debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de teléfono debe estar entre 11 y 15 caracteres.")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "Introduzca su usuario")]
        public string? UserName { get; set; }
    }
}
