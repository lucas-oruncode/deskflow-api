using Deskflow.API.Models;

namespace Deskflow.API.Repositories.Interfaces

{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(Guid id);
        Task CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
    }
}