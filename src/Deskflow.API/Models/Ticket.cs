using Deskflow.API.Enums;

namespace Deskflow.API.Models
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requester { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public string Solution { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
 
    }
}