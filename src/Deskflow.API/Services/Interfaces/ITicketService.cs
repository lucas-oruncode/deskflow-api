using Deskflow.API.Models;

namespace Deskflow.API.Services.Interfaces

{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(Guid id);
        Task CreateAsync(Ticket ticket);
    }
}