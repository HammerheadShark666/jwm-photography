using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using PhotographySite.Dto;
using PhotographySite.Dto.Response;
using System.Net;
using System.Text;

namespace PhotographySite.Middleware;

public static class GlobalExceptionHandlerExtension
{
    //This method will globally handle logging unhandled execeptions.
    //It will respond json response for ajax calls that send the json accept header
    //otherwise it will redirect to an error page
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app, 
                                                 ILogger logger, 
                                                 string errorPagePath, 
                                                 bool respondWithJsonErrorDetails = false)
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

                BaseResponse baseResponse = new BaseResponse()
                {
                    Messages = new List<Message>  {
                        new Message() { Severity = "error", Text = exception.Message}
                    }
                }; 

                var json = JsonConvert.SerializeObject(baseResponse); 

				logger.LogError(json);

				var matchText = "JSON";

				bool requiresJsonResponse = context.Request
													.GetTypedHeaders()
													.Accept
													.Any(t => t.Suffix.Value?.ToUpper() == matchText
															  || t.SubTypeWithoutSuffix.Value?.ToUpper() == matchText);  

				if (requiresJsonResponse)
                {
                    context.Response.ContentType = "application/json; charset=utf-8"; 
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