using Deskflow.API.Models;

namespace Deskflow.API.Repositories.Interfaces

{
    public interface IInteractionRepository
    {
        Task CreateAsync(Interaction interaction);
        Task<List<Interaction>> GetByTicketIdAsync (Guid ticketId);
    }
}