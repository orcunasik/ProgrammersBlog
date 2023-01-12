using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Services.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.LoadMyServices();
//builder.Services.AddDbContext<ProgrammersBlogContext>(x =>
//{
//    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
//    {
//        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ProgrammersBlogContext)).GetName().Name);
//    });
//});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
