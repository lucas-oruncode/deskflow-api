using Deskflow.API.DTOs.Interactions;
using Deskflow.API.Models;
using Deskflow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Deskflow.API.Controllers

{
    [ApiController]
    [Route("api/ticket/{ticketId:guid}/interactions")]
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInteraction(Guid ticketId, [FromBody] InteractionDto interactionDto)
        {
            var interaction = new Interaction
            {
               TicketId = ticketId,
               Message = interactionDto.Message,
               Author = interactionDto.Author
            };

            await _interactionService.CreateAsync(interaction);

            return Created($"/api/ticket/{ticketId}/interactions", 
                    new InteractionResponseDto
                    {
                        Id = interaction.Id,
                        TicketId = interaction.TicketId,
                        Message = interaction.Message,
                        Author = interaction.Author,
                        CreatedAt = interaction.CreatedAt
                    });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTicketInteractions(Guid ticketId)
        {
                var interactions = await _interactionService.GetByTicketIdAsync(ticketId);
                var interactionsDto = interactions.Select(interaction =>
                        new InteractionResponseDto
                        {
                            Id = interaction.Id,
                            TicketId = interaction.TicketId,
                            Message = interaction.Message,
                            Author = interaction.Author,
                            CreatedAt = interaction.CreatedAt
                        });
                return Ok(interactionsDto);
        }
    }
}