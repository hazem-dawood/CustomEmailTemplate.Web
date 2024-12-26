
var builder = WebApplication
    .CreateBuilder(args)
    .BuildApplication();

var app = builder.Build();

//don't forget to copy Rotativa Folder from wwwroot
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error")
        .UseHsts();
}

app.UseApplication()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
