using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhotographySite.Extensions;
using System.Net;
using System.Text;

namespace PhotographySite.Middleware;

public static class GlobalExceptionHandlerExtension
{
    //This method will globally handle logging unhandled execeptions.
    //It will respond json response for ajax calls that send the json accept header
    //otherwise it will redirect to an error page
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app
                                                , ILogger logger
                                                , string errorPagePath
                                                , bool respondWithJsonErrorDetails = false)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                //============================================================
                //Log Exception
                //============================================================
                var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                string errorDetails = $@"{exception.Message}
                                         {Environment.NewLine}
                                         {exception.StackTrace}";

                int statusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.StatusCode = statusCode;

                var problemDetails = new ProblemDetails
                {
                    Title = "Unexpected Error",
                    Status = statusCode,
                    Detail = errorDetails,
                    Instance = Guid.NewGuid().ToString()
                };

                var json = JsonConvert.SerializeObject(problemDetails);

                logger.LogError(json);
                 
                if (context.Request.IsAjaxRequest())
                {
                    context.Response.ContentType = "application/json; charset=utf-8";

                    if (!respondWithJsonErrorDetails)
                        json = JsonConvert.SerializeObject(new
                        {
                            Title = "Unexpected Error"                                                               ,
                            Status = statusCode
                        });
                    await context.Response
                                    .WriteAsync(json, Encoding.UTF8);
                }
                else
                {
                    context.Response.Redirect(errorPagePath);

                    await Task.CompletedTask;
                }
            });
        });
    } 
}