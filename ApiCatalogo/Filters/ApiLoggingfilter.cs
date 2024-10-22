using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters
{
    //Classe filtro que implementa a interface IActionfilter
    public class ApiLoggingFilter : IActionFilter
    {

        //Definindo a injeção do serviço definido em ILogger para ApiLoggingFilter
        /*
         * A interface Ilogger faz parte do sistema de registro, ou seja, do sistema de logging integrado do .NET, 
         * onde permite registrar informações mensagens e eventos. neste caso será na saida do console.
         */
        private readonly ILogger<ApiLoggingFilter> Logger;


        //injeta o serviço de Ilogger no construtor da classe
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            Logger = logger;
        }

        //Método que é executado antes da action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogInformation("...");
        }

        //Método que é executado após a action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogInformation("...");
        }
    }
}
