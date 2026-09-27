using System.ComponentModel.DataAnnotations;

namespace Deskflow.API.DTOs.Interactions

{
    public class InteractionDto
    {
        [Required]
        [StringLength(200)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Author { get; set; } = string.Empty;
    }
}