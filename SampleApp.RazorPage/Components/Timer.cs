using Microsoft.AspNetCore.Mvc;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Components
{
    public class Timer : ViewComponent
    {
        /*  public async Task<string> InvokeAsync()
        {
            return $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
        }*/

        public IViewComponentResult Invoke(User user)
        {
            return View("_User", user);
        }
    }
}
