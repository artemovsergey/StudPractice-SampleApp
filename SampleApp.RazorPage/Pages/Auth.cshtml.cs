using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Pages
{
    public class AuthModel : PageModel
    {

        private SampleContext _db;
        private IFlasher _f;
        public AuthModel(SampleContext db,IFlasher f)
        {
            _db = db;
            _f= f;
        }

        [BindProperty]
        public User Input { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {

            User current_user = _db.Users.Where(u => u.Email == Input.Email && u.Password == Input.HashPassword(Input.Password)).FirstOrDefault<User>() as User;
            if(current_user != null)
            {
                HttpContext.Session.SetString("SampleSession", $"{current_user.Id}");
     
                _f.Flash(Types.Success, $"Добро пожаловать, {current_user.Name}!", dismissable: true);
               
                return RedirectToPage("Index");
            }
            else
            {
                _f.Flash(Types.Danger, $"Неверный логин или пароль!", dismissable: true);
                return Page();
            }

        }

        public IActionResult OnGetLogout()
        {
            // сброс сессии
            HttpContext.Session.Clear();

            return RedirectToPage("Auth");
        }
    }
}
