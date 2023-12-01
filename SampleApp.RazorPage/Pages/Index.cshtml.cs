using Core.Flash;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IFlasher _f;
        private SampleContext _db;
        public IndexModel(ILogger<IndexModel> logger, IFlasher f, SampleContext db)
        {
            _logger = logger;
            _f = f;
            _db = db;
        }


        public User User { get; set; }
        public string sessionId { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            sessionId = HttpContext.Session.GetString("SampleSession");

            if (sessionId != null)
            {
                User = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(sessionId));
                return Page();
            }
            else
            {
                return RedirectToPage("Auth");
            }
            
        }

        public async Task<IActionResult> OnPostAsync(string message)
        {
            sessionId = HttpContext.Session.GetString("SampleSession");
            User = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(sessionId));

            if (!string.IsNullOrWhiteSpace(message))
            {
                var m = new Micropost()
                {
                    Content = message,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = this.User.Id,
                    User = this.User
                };


                try
                {
                    _db.Microposts.Add(m);
                    _db.SaveChanges();
                    _f.Flash(Types.Secondary, $"Tweet!", dismissable: true);
                    return RedirectToPage();
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, $"Ошибка создания сообщения: {ex.InnerException}");
                }


                return Page();
            }
            else
            {
                return Page();
            }

        }

        public async Task<IActionResult> OnGetDeleteAsync([FromQuery]int id)
        {
            sessionId = HttpContext.Session.GetString("SampleSession");
            User = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(sessionId));

            try
            {
                Micropost m = _db.Microposts.Find(id);
                _db.Microposts.Remove(m);
                _db.SaveChanges();
                _logger.Log(LogLevel.Error, $"Удалено сообщение \"{m.Content}\" пользователя {User.Name}!");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Ошибка удаления сообщения: {ex.InnerException}");
                _logger.Log(LogLevel.Error, $"Модель привязки из маршрута: {id}");
            }

            return Page();

        }

    }
}