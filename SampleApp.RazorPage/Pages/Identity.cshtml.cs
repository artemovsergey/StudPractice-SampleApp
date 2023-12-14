using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SampleApp.RazorPage.Pages
{
    public class IdentityModel : PageModel
    {
        public string UserIdentity { get; set; }

        public void OnGet()
        {
            UserIdentity = HttpContext.User.Identities.ToString();// User.ToString();

        }
    }
}
