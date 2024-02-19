namespace SampleApp.RazorPage.Middleware
{
    public class HeadersMiddleware
    {
        private readonly RequestDelegate _next;
        public HeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Content-Type-Options"] =
                "nosniff";
                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}

/*
 ASP.NET Core создает промежуточное ПО только
один раз за весь жизненный цикл вашего приложения, поэтому
любые зависимости, внедряемые в  конструктор, должны иметь
жизненный цикл Singleton. Если вам нужно использовать ограниченные или временные зависимости с  жизненными циклами
Scoped или Transient, внедрите их в метод Invoke 
 
 */
