using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using LocacaoFilmes.Domain.Validations;

namespace LocadoraFilmes.Domain.Entities
{
    public class Genero
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; private set; }

        [Required]
        public DateTime DataCriacao { get; private set; }

        public bool Ativo { get; private set; }

        public virtual ICollection<Filme> Filmes { get; set; }

        public Genero(int id, string nome, DateTime dataCriacao, bool ativo)
        {
            DomainExceptionValidation.When(id < 0, "O Id do gênero deve ser positivo");

            Id = id;
            ValidateDomain(nome, dataCriacao, ativo);
        }

        public Genero(string nome, DateTime dataCriacao, bool ativo)
        {
            ValidateDomain(nome, dataCriacao, ativo);
        }

        public void Update(string nome, DateTime dataCriacao, bool ativo)
        {
            ValidateDomain(nome, dataCriacao, ativo);
        }

        public void ValidateDomain(string nome, DateTime dataCriacao, bool ativo)
        {
            DomainExceptionValidation.When(nome.Length > 100, "O Nome deve ter no máximo 100 caracteres.");

            Nome = nome;
            DataCriacao = dataCriacao;
            Ativo = ativo;
        }
    }
}
