using Deskflow.API.Enums;
using Deskflow.API.Models;

namespace Deskflow.API.DTOs.Tickets

{
    public class TicketFilterDto
    {
        public TicketStatus? Status { get; set;}
        public TicketPriority? Priority { get; set; }
        public Guid? CategoryId { get; set; }
    }
}