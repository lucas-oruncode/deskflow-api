using System.ComponentModel.DataAnnotations;

namespace Deskflow.API.DTOs.Tickets
{
    public class CloseTicketDto
    {
        [Required(ErrorMessage = "A solução é obrigatória.")]
        [StringLength(500, ErrorMessage = "A solução não pode ter mais de 500 caracteres.")]
        public string Solution { get; set; } = string.Empty;
    }
}