using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SampleApp.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // On{verb}[handler][Async]
        public void OnGet()
        {

        }

        /* https://localhost:7246/?handler=SayHello как параметр строки запроса */
        /* https://localhost:7246/SayHello  как параметр маршрута @page "{handler}" */

        public void OnGetSayHello()
        {
            Console.WriteLine("Hello");
        }
    }
}