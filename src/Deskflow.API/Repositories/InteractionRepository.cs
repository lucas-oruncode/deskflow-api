using Deskflow.API.Data;
using Deskflow.API.Models;
using Deskflow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.API.Repositories

{
    public class InteractionRepository : IInteractionRepository
    {
        private readonly AppDbContext _context;

        public InteractionRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Interaction interaction)
        {
            _context.Interactions.Add(interaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Interaction>> GetByTicketIdAsync(Guid ticketId)
        {
            return await _context.Interactions
                                 .Where(interaction => interaction.TicketId == ticketId)
                                 .ToListAsync();
        }
    }
}