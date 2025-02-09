using Microsoft.EntityFrameworkCore;
using P10.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<P10DbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "Default",
    pattern: "{controller=Students}/{action=Index}/{id?}");
//app.MapGet("/", () => "Hello World!");

app.Run();
