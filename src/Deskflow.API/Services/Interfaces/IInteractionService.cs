using Deskflow.API.Models;

namespace Deskflow.API.Services.Interfaces

{
    public interface IInteractionService
    {
        Task CreateAsync(Interaction interaction);
        Task<List<Interaction>> GetByTicketIdAsync (Guid ticketId);
    }
}