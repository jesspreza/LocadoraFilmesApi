using System.Security.Claims;

namespace LocadoraFilmes.Infra.Ioc
{
    //Classe para obter os dados do usuário logado
    
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst("id").Value);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email").Value;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst("username").Value;
        }
    }
}
