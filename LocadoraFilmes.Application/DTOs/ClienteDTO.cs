using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        [MinLength(11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }
    }
}
