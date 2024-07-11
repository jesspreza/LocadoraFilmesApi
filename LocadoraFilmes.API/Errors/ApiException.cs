namespace LocadoraFilmes.API.Errors
{
    //Classe para retornar os erros em formato padrão
    public class ApiException
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiException(string statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
