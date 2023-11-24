using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;
using Vereyon.Web;

namespace SampleApp.RazorPage.Pages
{
    public class SignModel : PageModel
    {
        private readonly SampleContext _db;
        private IFlashMessage _flashMessage;
        private IFlasher _f;
        public SignModel(SampleContext db, IFlashMessage flashMessage, IFlasher f)
        {
            _db = db;
            _flashMessage = flashMessage;
            _f = f;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(Models.User user)
        {
           
            try
            {
                if (!user.IsPasswordConfirmation()){
                    _f.Flash(Types.Warning, $"Пароли должны совпадать!", dismissable: true);
                    return RedirectToPage("./Sign");
                }

                _db.Users.Add(user);
                _db.SaveChanges();
                
                _f.Flash(Types.Success, $"Пользователь {user.Name} зарегистрирован!", dismissable: true);
                return RedirectToPage("./Auth");

            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message.ToString();
                return RedirectToPage("./Sign");
            }

        }

    }
}
