using PhotographySite.Extensions;
using PhotographySite.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSession();
builder.Services.ConfigureDatabaseContext(builder.Configuration);
builder.Services.ConfigureMvc();
builder.Services.ConfigureControllers();
builder.Services.ConfigureScoped();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureApplicationInsights();

var app = builder.Build();

app.UseGlobalExceptionHandler(app.Logger, errorPagePath: "/error");
app.Logger.LogInformation("Starting Jwm Photography Website {DateTime.Now}", DateTime.Now);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.ConfigureRoutes();
app.UseStatusCodePagesWithRedirects("/error/{0}"); ///http

app.Run();