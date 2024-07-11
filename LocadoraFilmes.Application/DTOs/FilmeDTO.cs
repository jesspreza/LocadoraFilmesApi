using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class FilmeDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "O título do filme deve ser no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public bool Ativo { get; set; }
        
        [Required]
        public int GeneroId { get; set; }

        public GeneroDTO GeneroDTO { get; set; }
    }
}
