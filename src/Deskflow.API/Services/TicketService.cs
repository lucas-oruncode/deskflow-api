using Deskflow.API.Enums;
using Deskflow.API.Models;
using Deskflow.API.Repositories.Interfaces;
using Deskflow.API.Services.Interfaces;

namespace Deskflow.API.Services

{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TicketService(ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
        {
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentException("O chamado não pode ser nulo");
            }

            if(ticket.CategoryId == Guid.Empty)
            {
                throw new ArgumentException($"Categoria não pode ser nula.");
            }
            
            var category = await _categoryRepository.GetByIdAsync(ticket.CategoryId);
            
            if (category is null)
            {
                throw new KeyNotFoundException(
                    $"Categoria com ID {ticket.CategoryId} não encontrada.");
            }

            ticket.Status = TicketStatus.Open;
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.ClosedAt = null;
            ValidateTitle(ticket.Title);
            ValidateDescription(ticket.Description);
            
            
            await _ticketRepository.CreateAsync(ticket);
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID do chamado não pode ser nulo.");
            }
            
            var ticket = await _ticketRepository.GetByIdAsync(id);
            
            if (ticket is null)
            {
                throw new KeyNotFoundException($"Chamado com ID {id} não encontrado.");
            }
            
            return ticket;
        }

        public async Task StartAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID do chamado não pode ser nulo.");
            }

            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Chamado com ID {id} não encontrado");
            }

            if (ticket.Status != TicketStatus.Open)
            {
                throw new InvalidOperationException("O chamado só pode ser iniciado quando estiver aberto.");
            }

            ticket.Status = TicketStatus.InProgress;

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task CloseAsync(Guid id, string solution)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID do chamado não pode ser vazio.");
            }

            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Chamado com ID {id} não encontrado");
            }

            if (ticket.Status != TicketStatus.InProgress)
            {
                throw new InvalidOperationException("O chamado só pode ser encerrado quando estiver em progresso.");
            }
            
            if (string.IsNullOrWhiteSpace(solution))
            {
                throw new ArgumentException("A solução do chamado é obrigatória.");
            }

            if (solution.Trim().Length > 500)
            {
                throw new ArgumentException("A solução não pode ter mais de 500 caracteres.");
            }

            ticket.Status = TicketStatus.Closed;
            ticket.Solution = solution.Trim();
            ticket.ClosedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);   
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("O titulo do chamado não pode ser nulo ou vazio.");
            }
            if(title.Trim().Length > 50)
            {
                throw new ArgumentException("O titulo do chamado não pode ter mais de 50 caracteres.");
            }
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("A descrição do chamado não pode ser nulo ou vazio.");
            }
            if(description.Trim().Length > 200)
            {
                throw new ArgumentException("A descrição do chamado não pode ter mais de 200 caracteres.");
            }
        }

    }
}