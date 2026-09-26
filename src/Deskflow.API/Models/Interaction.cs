namespace Deskflow.API.Models

{
    public class Interaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}