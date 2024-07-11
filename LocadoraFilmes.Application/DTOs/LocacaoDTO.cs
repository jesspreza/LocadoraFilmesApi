using LocadoraFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class LocacaoDTO
    {
        public int Id { get; set; }
        [Required]
        public int ClienteId { get; set; }

        public ClienteDTO ClienteDTO { get; set; }
        [Required]
        public DateTime DataLocacao { get; set; }
        [Required]
        public DateTime DataDevolucao { get; set; }
        [Required]
        public bool Entregue { get; set; }

        public ICollection<LocacaoFilmeDTO> LocacaoFilmesDTO { get; set; }
    }
}
