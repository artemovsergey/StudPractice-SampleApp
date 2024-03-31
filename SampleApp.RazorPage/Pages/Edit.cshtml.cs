using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;


namespace SampleApp.RazorPage.Pages
{
    public class EditModel : PageModel
    {

        private readonly SampleAppContext _db;
        private readonly ILogger<EditModel> _log;
        private readonly IFlasher _f;
        public EditModel(SampleAppContext db, ILogger<EditModel> log, IFlasher f)
        {
            _db = db;
            _log = log;
            _f = f;
        }

        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
            var sessionId = Convert.ToInt32(HttpContext.Session.GetString("SampleSession"));
            User = _db.Users.Find(sessionId);
        }

        public void OnPost()
        {
            _db.Update(User);
            //_db.Entry(User).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
                _log.LogError($"Пользователь обновлен!");
                _f.Primary($"Пользователь обновлен!");
            }
            catch (Exception ex)
            {
                _log.LogError($"Ошибка: {ex.InnerException.Message}");
                _f.Danger($"Ошибка: {ex.InnerException.Message}");
            }
        }
    }
}