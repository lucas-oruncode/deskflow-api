using Deskflow.API.DTOs.Categories;
using Deskflow.API.DTOs.Interactions;

namespace Deskflow.API.DTOs.Tickets
{
    public class TicketDetailsResponseDto : TicketResponseDto
    {
        public CategoryResponseDto Category { get; set; }
        public List<InteractionResponseDto> Interactions { get; set; } = new();
    }
}