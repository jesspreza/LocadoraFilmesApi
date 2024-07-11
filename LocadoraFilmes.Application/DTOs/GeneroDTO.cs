using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class GeneroDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O nome do gênero deve ser no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
