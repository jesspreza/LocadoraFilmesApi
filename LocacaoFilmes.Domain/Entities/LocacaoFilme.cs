using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Domain.Entities
{
    public class LocacaoFilme
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira para locação
        public int LocacaoId { get; set; }

        public virtual Locacao Locacao { get; set; }

        // Chave estrangeira para filme
        public int FilmeId { get; set; }

        public virtual Filme Filme { get; set; }
    }
}
