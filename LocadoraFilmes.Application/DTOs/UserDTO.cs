using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LocadoraFilmes.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "O nome de usuário deve ter no mínimo 3 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [MaxLength(200, ErrorMessage = "O email deve ter no máximo 200 caracteres")]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo 100 caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
        [NotMapped]
        public string Password { get; set; }

        [JsonIgnore]
        public bool IsAdmin { get; set; }
    }
}
