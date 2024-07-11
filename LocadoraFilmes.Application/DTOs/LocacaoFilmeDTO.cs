using LocadoraFilmes.Domain.Entities;
using System.Text.Json.Serialization;

namespace LocadoraFilmes.Application.DTOs
{
    public class LocacaoFilmeDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int LocacaoId { get; set; }
        [JsonIgnore]
        public Locacao Locacao { get; set; }

        [JsonIgnore]
        public int FilmeId { get; set; }

        public FilmeDTO FilmeDTO { get; set; }
    }
}
