using Deskflow.API.Data;
using Deskflow.API.DTOs.Tickets;
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

        public async Task<List<Ticket>> GetAllAsync(TicketFilterDto filters)
        {
            var query = _context.Tickets
                                  .Include(t => t.Category)
                                  .Include(t => t.Interactions)
                                  .AsQueryable();
            
            if (filters.Status.HasValue)
            {
                query = query.Where(t => t.Status == filters.Status.Value);
            }
            
            if (filters.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == filters.Priority.Value);
            }
            
            if (filters.CategoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == filters.CategoryId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            return await _context.Tickets
                                 .Include(t => t.Category)
                                 .Include(t => t.Interactions)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}