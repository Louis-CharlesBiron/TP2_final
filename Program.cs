using System.Net.Http.Headers;

namespace TP2_final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/NonConnecte/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "admin",
                pattern: "administrateur/{action=Catalogue}/{controller=Admin}",
                constraints: new { controller = "^Admin$" }
            );

            app.MapControllerRoute(
                name: "user",
                pattern: "{action=Index}/utilisateur/{controller=User}",
                constraints: new { controller = "^User$" }
            );

            app.MapControllerRoute(
                name: "nonconnecte",
                pattern: "{action=Index}/{controller=NonConnected}/notLogged",
                constraints: new { controller = "^NonConnecte$" }
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=NonConnecte}/{action=Index}/{id?}"
                //constraints: new {controller = "^(NonConnecte)$"}
            );

            app.Run();
        }
    }
}