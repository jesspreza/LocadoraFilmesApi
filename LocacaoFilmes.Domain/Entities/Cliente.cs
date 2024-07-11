using LocacaoFilmes.Domain.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Nome { get; private set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; private set; }

        public virtual ICollection<Locacao> Locacoes { get; set; }

        public Cliente(int id, string nome, string cpf)
        {
            DomainExceptionValidation.When(id < 0, "O Id do cliente deve ser positivo.");
            
            Id = id;
            ValidateDomain(nome, cpf);
        }

        public Cliente(string nome, string cpf)
        {
            ValidateDomain(nome, cpf);
        }

        public void Update(string nome, string cpf)
        {
            ValidateDomain(nome, cpf);
        }

        public void ValidateDomain(string nome, string cpf)
        {
            DomainExceptionValidation.When(cpf.Length != 11, "O CPF deve ter 11 caracteres.");
            DomainExceptionValidation.When(nome.Length > 200, "O Nome deve ter no máximo 200 caracteres.");
            DomainExceptionValidation.When(nome.Length < 3, "O Nome deve ter no mínimo 3 caracteres.");
            
            Nome = nome;
            Cpf = cpf;
        }
    }
}
