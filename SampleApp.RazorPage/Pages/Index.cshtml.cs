using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SampleApp.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string Param { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Number { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // On{verb}[handler][Async]
        public void OnGet(string param)
        {
            _logger.LogInformation($"Параметр метода:{param}");
            _logger.LogInformation($"Привязанное свойство Param:{Param}");
            _logger.LogInformation($"Значение маршрута Number:{Number}");
            _logger.LogInformation($"Модель состояния ModelState:{ModelState.IsValid}");
            _logger.LogInformation($"Request Path:{HttpContext.Request.Path.Value}");
        }

        /* https://localhost:7246/?handler=SayHello как параметр строки запроса */
        /* https://localhost:7246/SayHello  как параметр маршрута @page "{handler}" */

        public void OnGetSayHello()
        {
            Console.WriteLine("Hello");
        }

        //public IActionResult OnGetPartial() => Partial("_Header");



    }
}