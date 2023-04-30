
namespace SCAGEUsers.Application.Utils
{
    public class RequestResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public static string Error(TypeAction typeAction, string message)
        {
            return "Erro ao "+ typeAction.ToString() +": " + message;
        }

        public static RequestResponse New(string message, object data)
        {
            var response = new RequestResponse
            {
                Message = message,
                Data = data
            };
            return response;
        }
    }

    public enum TypeAction
    {
        Criar = 0,
        Atualizar = 1,
        Obter = 2,
        Deletar = 3,
    }
}
