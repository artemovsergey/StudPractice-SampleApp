using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleApp.RazorPage.Pages
{
    public class SignModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string User { get; set; }


        public void OnGet()
        {
            
        }

        public void OnPostHandler()
        {

        }

    }
}
