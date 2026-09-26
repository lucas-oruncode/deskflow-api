using Deskflow.API.Enums;

namespace Deskflow.API.DTOs.Tickets

{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requester { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? ClosedAt { get; set; }
    }
}