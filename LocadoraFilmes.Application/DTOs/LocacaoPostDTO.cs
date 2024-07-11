using LocadoraFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace LocadoraFilmes.Application.DTOs
{
    public class LocacaoPostDTO
    {
        [Required(ErrorMessage = "É obrigatório informar o cliente")]
        [Range(1, int.MaxValue, ErrorMessage = "O identificador do cliente é inválido")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a data da devolução")]
        public DateTime DataDevolucao { get; set; }

        [Required(ErrorMessage = "É obrigatório informar pelo menos um filme")]
        public List<int> FilmesId { get; set; } = Enumerable.Empty<int>().ToList();
        
        [JsonIgnore]
        public DateTime DataLocacao { get; set; }

        [JsonIgnore]
        public bool Entregue { get; set; }
    }
}
