using System.Security;

namespace LocadoraFilmes.API.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public bool IsAdmin {  get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
