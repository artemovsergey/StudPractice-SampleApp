using Core.Flash;
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
        private IFlasher _f;
        public IndexModel(ILogger<IndexModel> logger, IFlasher f)
        {
            _logger = logger;
            _f = f;
        }

        // On{verb}[handler][Async]
        public void OnGet(string param)
        {

        }

        /* https://localhost:7246/?handler=SayHello как параметр строки запроса */
        /* https://localhost:7246/SayHello  как параметр маршрута @page "{handler}" */

    }
}