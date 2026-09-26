using Deskflow.API.Data;
using Deskflow.API.Models;
using Deskflow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deskflow.API.Repositories

{
    public class TicketRepository : ITicketRepository
    {

        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            var tickets = await _context.Tickets
                                        .Include(t => t.Category)
                                        .ToListAsync();
            return tickets;
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            return await _context.Tickets
                                 .Include(t => t.Category)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}