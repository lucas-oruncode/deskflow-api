using Deskflow.API.DTOs.Categories;
using Deskflow.API.DTOs.Interactions;
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
        public async Task<IActionResult> GetAllTickets([FromQuery] TicketFilterDto filters)
        {
            var tickets = await _ticketService.GetAllAsync(filters);
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id);            
                return Ok( new TicketDetailsResponseDto
                    {
                        Id = ticket.Id,
                        Title = ticket.Title,
                        Description = ticket.Description,
                        Requester = ticket.Requester,
                        Status = ticket.Status,
                        Priority = ticket.Priority,
                        CategoryId = ticket.CategoryId,
                        CreatedAt = ticket.CreatedAt,
                        ClosedAt = ticket.ClosedAt,
                        Category = new CategoryResponseDto
                        {
                            Id = ticket.Category.Id,
                            Name = ticket.Category.Name
                        },
                        Interactions = ticket.Interactions
                                             .Select(i =>
                                             new InteractionResponseDto
                                             {
                                                 Id = i.Id,
                                                 TicketId = i.TicketId,
                                                 Message = i.Message,
                                                 Author = i.Author,
                                                 CreatedAt = i.CreatedAt
                                             }).ToList()
                    }
                );            
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/start")]
        public async Task<IActionResult> StartTicket(Guid id)
        {
            try
            {
                await _ticketService.StartAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id:guid}/close")]
        public async Task<IActionResult> CloseTicket(Guid id, [FromBody] CloseTicketDto closeTicketDto)
        {
            try
            {
                await _ticketService.CloseAsync(id, closeTicketDto.Solution);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}