namespace Deskflow.API.DTOs.Interactions

{
    public class InteractionResponseDto
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}