using System.ComponentModel.DataAnnotations;

namespace Deskflow.API.DTOs.Interactions

{
    public class InteractionDto
    {
        [Required(ErrorMessage ="Mensagem é obrigatória")]
        [StringLength(200, ErrorMessage = "A mensagem não pode ter mais de 200 caracteres")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage ="O autor é obrigatória")]
        [StringLength(50, ErrorMessage = "O nome do autor não pode ter mais de 50 caracteres")]
        public string Author { get; set; } = string.Empty;
    }
}