using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using SampleApp.RazorPage.Middleware;
using SampleApp.RazorPage.Models;
using Serilog;
using System.Configuration;
namespace SampleApp.RazorPage
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSerilog();
            builder.Logging.AddSeq();

            builder.Services.AddRazorPages();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation(
             x => x.DisableDataAnnotationsValidation = true)
             .AddFluentValidationClientsideAdapters();

            ValidatorOptions.Global.LanguageManager.Enabled = false;



            builder.Services.AddHealthChecks();
            #if DEBUG
            builder.Services.AddSassCompiler();
            #endif

            builder.Services.AddHttpContextAccessor();

            // @inject IHttpContextAccessor httpContextAccessor

            // Подключение базы данных SQL Server
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            //builder.Services.AddDbContext<SampleContext>(options => options.UseNpgsql(connection));
            builder.Services.AddDbContext<SampleContext>(options => options.UseSqlServer(connection));
            builder.Services.AddFlashes();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options => builder.Configuration.Bind("JwtSettings", options))

                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options => builder.Configuration.Bind("CookieSettings", options));


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                "CanEnterSecurity",
                policyBuilder => policyBuilder.RequireClaim("BoardingPassNumber"));
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
            });



            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SampleSession";
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddCors(options => {
                options.AddPolicy("AllowShoppingApp", policy =>
                policy.WithOrigins("http://shopping.com")
                .AllowAnyMethod());
            });

            builder.Logging.AddFile();

                var app = builder.Build();


                app.UseMiddleware<HeadersMiddleware>();

            //app.Run(async (HttpContext context) =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync(
            //    DateTimeOffset.UtcNow.ToString());
            //});

            app.Map("/ping", (IApplicationBuilder branch) =>
                {
                    //branch.UseExceptionHandler();
                    branch.Run(async (HttpContext context) =>
                    {
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("pong");
                    });
                });

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

                //app.UseCors("AllowShoppingApp");

                app.UseAuthorization();

                app.UseSession();
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


                app.UseSerilogRequestLogging();

            app.Run();


        }

        public static void AddAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder config)
            {
                config.AddJsonFile(
                "appsettings.json",
                optional: true,
                reloadOnChange: true);
                config.AddUserSecrets<Program>();
            }

        
    }
}