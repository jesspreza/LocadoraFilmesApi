using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocadoraFilmes.Application.DTOs
{
    public class FilmePutDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "É obrigatório informar o título do filme")]
        [MaxLength(200, ErrorMessage = "O título do filme deve ser no máximo 100 caracteres")]
        public string Nome { get; set; }

       [Required]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o gênero do filme")]
        [Range(1, int.MaxValue)]
        public int GeneroId { get; set; }
    }
}
