using LocacaoFilmes.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Domain.Entities
{
    public class User
    {
        [Key]
        public long Id { get; private set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string UserName { get; private set; }

        [Required]
        public string Password { get; private set; }

        [MaxLength(200)]
        [Required]
        public string Email { get; private set; }

        public byte[] PasswordHash { get; private set; }

        public byte[] PasswordSalt { get; private set; }

        [Required]
        public bool IsAdmin { get; private set; }

        public User(int id, string userName, string email)
        {
            DomainExceptionValidation.When(id < 0, "O id não pode ser negativo");
            Id = id;
            ValidateDomain(userName, email);
        }

        public User(string userName, string email)
        {
            ValidateDomain(userName, email);
        }

        public void AlterarSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private void ValidateDomain(string userName, string email)
        {
            DomainExceptionValidation.When(userName.Length > 200, "O nome de usuário não pode ser maior que 200 caracteres");
            DomainExceptionValidation.When(userName.Length < 3, "O nome de usuário não pode ser menor que 3 caracteres");
            DomainExceptionValidation.When(userName == null, "O nome de usuário é obrigatório");
            DomainExceptionValidation.When(email.Length > 200, "O email não pode ser maior que 200 caracteres");
            DomainExceptionValidation.When(email == null, "O email é obrigatório");
            
            UserName = userName;
            Email = email;
        }
    }
}
