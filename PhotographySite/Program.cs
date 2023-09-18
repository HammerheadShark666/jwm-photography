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
 
var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseGlobalExceptionHandler(app.Logger, errorPagePath: "/error", respondWithJsonErrorDetails: true);  

app.Logger.LogInformation("Starting Jwm Photography Website {0}", DateTime.Now); 
app.UseHttpsRedirection(); 
 
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization(); 
app.UseSession(); 
app.ConfigureRoutes();
    
app.Run();