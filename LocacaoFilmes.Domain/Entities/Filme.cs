using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using LocacaoFilmes.Domain.Validations;

namespace LocadoraFilmes.Domain.Entities
{
    public class Filme
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; private set; }

        [Required]
        public DateTime DataCriacao { get; private set; }

        public bool Ativo { get; private set; }

        // Chave estrangeira para o gênero
        public int GeneroId { get; private set; }
        public Genero Genero { get; set; }

        // Relacionamento muitos para muitos com locações através de uma tabela intermediária
        public virtual ICollection<LocacaoFilme> LocacaoFilmes { get; set; }

        public Filme(int id, string nome, DateTime dataCriacao, bool ativo, int generoId)
        {
            DomainExceptionValidation.When(id < 0, "O Id do filme deve ser positivo");
            
            Id = id;
            DataCriacao = dataCriacao;
            ValidateDomain(nome, ativo, generoId);
        }

        public Filme(string nome, DateTime dataCriacao, bool ativo, int generoId)
        {
            DataCriacao = dataCriacao;
            ValidateDomain(nome, ativo, generoId);
        }

        public void Update(string nome, bool ativo, int generoId)
        {
            ValidateDomain(nome, ativo, generoId);
        }

        public void ValidateDomain(string nome, bool ativo, int generoId)
        {
            DomainExceptionValidation.When(nome.Length > 200, "O Nome deve ter no máximo 200 caracteres.");
            DomainExceptionValidation.When(generoId < 0, "O Id do genero deve ser positivo.");

            Nome = nome;
            Ativo = ativo;
            GeneroId = generoId;
        }
    }
}
