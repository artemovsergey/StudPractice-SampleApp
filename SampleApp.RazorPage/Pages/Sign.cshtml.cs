using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SampleApp.RazorPage.Pages
{
    public class SignModel : PageModel
    {

        public string Item { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }
         = new List<SelectListItem>
         {
         new SelectListItem{Value= "AllLanguage", Text="Все языки"},
         new SelectListItem{Value= "csharp", Text="C#"},
         new SelectListItem{Value= "python", Text= "Python"},
         new SelectListItem{Value= "cpp", Text="C++"},
         new SelectListItem{Value= "java", Text="Java"},
         new SelectListItem{Value= "js", Text="JavaScript"},
         new SelectListItem{Value= "ruby", Text="Ruby"},
         };




        private readonly SampleContext _db;
        private IFlasher _f;
        private ILogger<SignModel> _logger;
        public SignModel(SampleContext db, IFlasher f, ILogger<SignModel> logger)
        {
            _db = db;
            _f = f;
            _logger = logger;
        }


        [BindProperty]
        public User Input { get; set; }

        //async
        public void OnGet()
        {
            
        }

        //async
        public IActionResult OnPost(User Input) // либо через свойство, либо через параметр
        {

            //ModelState.AddModelError(string.Empty,"Cannot convert currency to itself");

            if (!ModelState.IsValid)
            {
                _f.Flash(Types.Danger, $"Валидация не пройдена!", dismissable: true);
                return Page();
            }

            if (!Input.IsPasswordConfirmation())
            {
                _f.Flash(Types.Warning, $"Пароли должны совпадать!", dismissable: true);
                return Page();
            }

            Input.Password = Input.HashPassword(Input.Password);
            Input.PasswordConfirmation = Input.HashPassword(Input.PasswordConfirmation);

            try
            {
                _db.Users.Add(Input);
                _db.SaveChanges();

                _f.Flash(Types.Success, $"Пользователь {Input.Name} зарегистрирован!", dismissable: true);
                return RedirectToPage("./Auth");

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return RedirectToPage("./Sign");
            }
        }

    }

}
