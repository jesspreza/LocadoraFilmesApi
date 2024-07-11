using LocadoraFilmes.Domain.Entities;
using System.Threading.Tasks;

namespace LocacaoFilmes.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> UserExists(string email);
        public string GenerateToken(long id, string email);
        public Task<User> GetUserByEmail(string email);
    }
}
