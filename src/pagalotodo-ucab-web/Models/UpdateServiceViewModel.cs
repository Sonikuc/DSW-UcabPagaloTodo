using System.ComponentModel.DataAnnotations;

namespace UCABPagaloTodoWeb.Models
{
    public class UpdateServiceViewModel
    {
        public string ServiceName { get; set; }

        public string? TypeService { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El número de teléfono debe ser un valor numérico.")]
        [StringLength(15, MinimumLength = 11, ErrorMessage = "La longitud del número de teléfono debe estar entre 11 y 15 caracteres.")]
        public string? ContactNumber { get; set; }

        public string? Username { get; set; }


        public UpdateServiceViewModel()
        {
            ServiceName = "";
            TypeService = "";
            ContactNumber = "";
        }
    }
}
