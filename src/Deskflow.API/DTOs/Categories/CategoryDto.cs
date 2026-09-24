using System.ComponentModel.DataAnnotations;

namespace Deskflow.API.DTOs.Categories

{
    public class CategoryDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }
}