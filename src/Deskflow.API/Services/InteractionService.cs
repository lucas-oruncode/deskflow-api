using Deskflow.API.Enums;
using Deskflow.API.Models;
using Deskflow.API.Repositories.Interfaces;
using Deskflow.API.Services.Interfaces;

namespace Deskflow.API.Services

{
    public class InteractionService : IInteractionService
    {

        private readonly IInteractionRepository _interactionRepository;
        private readonly ITicketRepository _ticketRepository;

        public InteractionService (IInteractionRepository interactionRepository, ITicketRepository ticketRepository)
        {
            _interactionRepository = interactionRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task CreateAsync(Interaction interaction)
        {
            if (interaction == null)
            {
                throw new ArgumentException("A interação não pode estar vazia");
            }
            
            if (interaction.TicketId == Guid.Empty)
            {
                throw new ArgumentException("O chamado não pode estar vazio.");
            }

            var ticket = await _ticketRepository.GetByIdAsync(interaction.TicketId);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"O chamado com ID {interaction.TicketId} não existe.");
            }

            if (ticket.Status == TicketStatus.Closed)
            {
                throw new InvalidOperationException("O chamado está fechado, não pode mais haver interações.");
            }
            
            if (string.IsNullOrWhiteSpace(interaction.Author))
            {
                throw new ArgumentException("O autor deve ser preenchido.");
            }
            
            if (interaction.Author.Trim().Length > 50)
            {
                throw new ArgumentException("O autor não pode ter mais de 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(interaction.Message))
            {
                throw new ArgumentException("A mensagem deve ser preenchida");
            }

            if (interaction.Message.Trim().Length > 200)
            {
                throw new ArgumentException("A mensagem não pode ter mais de 200 caracteres.");
            }

            interaction.Author = interaction.Author.Trim();
            interaction.Message = interaction.Message.Trim();
            interaction.CreatedAt = DateTime.UtcNow;

            await _interactionRepository.CreateAsync(interaction);
        }

        public async Task<List<Interaction>> GetByTicketIdAsync(Guid ticketId)
        {
            if (ticketId == Guid.Empty)
            {
                throw new ArgumentException("O ID do chamado deve ser preenchido.");
            }

            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"O chamado com ID {ticketId} não existe.");
            }

            var interactions = await _interactionRepository.GetByTicketIdAsync(ticketId);

            return interactions;
        }
    }
}