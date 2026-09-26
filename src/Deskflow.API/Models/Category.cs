namespace Deskflow.API.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}