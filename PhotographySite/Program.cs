using PhotographySite.Middleware;
using PhotographySite.SetUp;
using PhotographySite.StartUp;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllersWithViews();

SetUpDatabaseContext.Setup(builder);
SetUpMvc.Setup(builder);
SetUpController.Setup(builder); 
SetUpScoped.Setup(builder); 
SetUpAutoMapper.Setup(builder); 
SetUpFluentValidation.Setup(builder);
SetUpIdentity.Setup(builder);
 
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseGlobalExceptionHandler(app.Logger, errorPagePath: "/error", respondWithJsonErrorDetails: true); 
} 

app.Logger.LogInformation("Starting Jwm Photography Website {0}", DateTime.Now); 
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); 
app.UseAuthentication();
app.UseAuthorization(); 

SetUpRoutes.Setup(app);
    
app.Run();