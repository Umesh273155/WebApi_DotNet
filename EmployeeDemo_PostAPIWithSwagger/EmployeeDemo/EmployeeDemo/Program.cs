
using EmployeeDemo.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Octokit;
using System.ComponentModel.Design;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddScoped<ConnectionString>();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<RedirectRoleMiddleWare>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{Id?}");
});
app.Run();
