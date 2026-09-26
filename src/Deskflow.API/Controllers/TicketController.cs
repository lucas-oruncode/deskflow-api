using Deskflow.API.DTOs.Tickets;
using Deskflow.API.Models;
using Deskflow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Deskflow.API.Controllers

{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                Requester = ticketDto.Requester,
                Priority = ticketDto.Priority,
                CategoryId = ticketDto.CategoryId
            };

            await _ticketService.CreateAsync(ticket);
            return Created($"/api/ticket/{ticket.Id}", 
                    new TicketResponseDto
                    {
                       Id = ticket.Id,
                       Title = ticket.Title,
                       Description = ticket.Description,
                       Requester = ticket.Requester,
                       Status = ticket.Status,
                       Priority = ticket.Priority,
                       CategoryId = ticket.CategoryId,
                       CreatedAt = ticket.CreatedAt,
                       ClosedAt = ticket.ClosedAt
                    });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllAsync();
            var ticketDtos = tickets.Select(ticket => 
                    new TicketResponseDto
                    {
                       Id = ticket.Id,
                       Title = ticket.Title,
                       Description = ticket.Description,
                       Requester = ticket.Requester,
                       Status = ticket.Status,
                       Priority = ticket.Priority,
                       CategoryId = ticket.CategoryId,
                       CreatedAt = ticket.CreatedAt,
                       ClosedAt = ticket.ClosedAt
                    }).ToList();
            
            return Ok(ticketDtos);
        }
        
    }
}