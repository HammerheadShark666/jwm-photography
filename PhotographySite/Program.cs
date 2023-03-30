using PhotographySite.Middleware;
using PhotographySite.SetUp;
using PhotographySite.StartUp;

var builder = WebApplication.CreateBuilder(args);

SetUpDatabaseContext.Setup(builder);
SetUpMvc.Setup(builder);
SetUpController.Setup(builder); 
SetUpScoped.Setup(builder); 
SetUpAutoMapper.Setup(builder); 
SetUpFluentValidation.Setup(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseExceptionHandler(err => err.UseCustomErrors(app.Environment));

app.Logger.LogInformation("Log some information #1");
app.UseGlobalExceptionHandler(app.Logger, errorPagePath: "/error", respondWithJsonErrorDetails: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
 
SetUpRoutes.Setup(app);
    
app.Run();