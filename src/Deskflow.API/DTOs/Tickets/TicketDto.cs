using System.ComponentModel.DataAnnotations;
using Deskflow.API.Enums;

namespace Deskflow.API.DTOs.Tickets

{
    public class TicketDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(50, ErrorMessage = "O título não pode ter mais de 50 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição não pode ter mais de 200 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O solicitante é obrigatório.")]
        [StringLength(50, ErrorMessage = "O solicitante não pode ter mais de 50 caracteres.")]
        public string Requester { get; set; }

        public TicketPriority Priority { get; set; }
        public Guid CategoryId { get; set; }
    }
}