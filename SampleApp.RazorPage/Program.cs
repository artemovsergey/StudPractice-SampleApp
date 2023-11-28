using Microsoft.EntityFrameworkCore;
using SampleApp.RazorPage.Models;


namespace SampleApp.RazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddHealthChecks();
            #if DEBUG
            builder.Services.AddSassCompiler();
            #endif

            builder.Services.AddHttpContextAccessor();

            // @inject IHttpContextAccessor httpContextAccessor

            // Подключение базы данных SQL Server
            string connection = builder.Configuration.GetConnectionString("PostgreSQL");
            builder.Services.AddDbContext<SampleContext>(options => options.UseNpgsql(connection));

            builder.Services.AddFlashes();


            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SampleSession";
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });





            var app = builder.Build();

            //app.UseWelcomePage();

            #region Middleware Error
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            } 
            #endregion

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            
            app.UseSession();


            /*  Регистрируем все
                страницы Razor
                в приложении
                в качестве
                конечных точек. */

            app.MapRazorPages();

            /* Регистрируем встроенную конечную точку, которая
               возвращает «Hello World!» на маршруте /test. */

            app.MapGet("/test", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });


            /* Регистрируем конечную точку проверки
               работоспособности на маршруте /healthz. */
            app.MapHealthChecks("/healthz");




            app.Run();
        }
    }
}