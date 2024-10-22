using ApiCatalogo.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
namespace ApiCatalogo.Extensions;
    public static class ApiExceptionMiddlerareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            //Configura o middleware de tratamento recebendo um delegate que será executado quando uma exceção não tratada ocorrer
            app.UseExceptionHandler(appError =>
            {
                //Cria um contexto de resposta
                appError.Run(async context =>{
                    //define um código de resposta http
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //define o tipo de conteudo da resposta
                    context.Response.ContentType = "application/json";

                    //obtemos uma caracteristica do manipulador de exceções
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        //escrevemos os detalhes da exceção no formato json 
                        await context.Response.WriteAsync(new ErrorDatails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Mensage = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace
                        }.ToString());
                    }

                });
         });

        }
    }

