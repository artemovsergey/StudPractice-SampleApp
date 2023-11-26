using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;
using System.Security.Cryptography;
using System.Text;


namespace SampleApp.RazorPage.Pages
{
    public class SignModel : PageModel
    {
        private readonly SampleContext _db;
        private IFlasher _f;

       
        public SignModel(SampleContext db, IFlasher f)
        {
            _db = db;
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


                user.Password = user.HashPassword(user.Password);
                user.PasswordConfirmation = user.HashPassword(user.PasswordConfirmation);

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
