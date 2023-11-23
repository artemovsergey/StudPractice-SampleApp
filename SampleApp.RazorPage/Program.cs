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