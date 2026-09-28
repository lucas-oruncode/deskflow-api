using Deskflow.API.DTOs.Tickets;
using Deskflow.API.Models;

namespace Deskflow.API.Repositories.Interfaces

{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync(TicketFilterDto filters);
        Task<Ticket> GetByIdAsync(Guid id);
        Task CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
    }
}