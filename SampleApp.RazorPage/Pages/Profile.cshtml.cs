using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;


namespace SampleApp.RazorPage.Pages;

public class ProfileModel : PageModel
{

    private readonly SampleAppContext _db;
    private readonly ILogger<ProfileModel> _log;
    private readonly IFlasher _f;

    public User ProfileUser { get; set; }
    public User CurrentUser { get; set; }

    public bool IsFollow { get; set; }

    public ProfileModel(SampleAppContext db, ILogger<ProfileModel> log, IFlasher f)
    {
        _db = db;
        _log = log;
        _f = f;
    }

    public async Task<IActionResult> OnGetAsync([FromRoute] int? id)
    {
        var sessionId = HttpContext.Session.GetString("SampleSession");
        ProfileUser = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == id) as User;
        CurrentUser = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id.ToString() == sessionId) as User;

        // если текущий пользователь подписан на профиль пользователя
        var result = _db.Relations.Where(r => r.Follower == CurrentUser && r.Followed == ProfileUser).FirstOrDefault();

        if (result != null)
        {
            IsFollow = true;
        }
        else
        {
            IsFollow = false;
        }

        return Page();
    }


    public async Task<IActionResult> OnPostAsync([FromRoute] int? id)
    {
        var sessionId = HttpContext.Session.GetString("SampleSession");
        ProfileUser = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == id) as User;
        CurrentUser = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id.ToString() == sessionId) as User;

        // если текущий пользователь подписан на профиль пользователя
        var result = _db.Relations.Where(r => r.Follower == CurrentUser && r.Followed == ProfileUser).FirstOrDefault();

        if (result != null)
        {
            IsFollow = true;
        }
        else
        {
            IsFollow = false;
        }

        if (IsFollow == false)
        {
            try
            {
                _db.Relations.Add(new Relation() { FollowerId = CurrentUser.Id, FollowedId = ProfileUser.Id });
                _db.SaveChanges();
                _f.Flash(Types.Success, $"Пользователь {CurrentUser.Name} подписался на {ProfileUser.Name}!");
            }
            catch (Exception ex)
            {
                _f.Flash(Types.Success, $"{ex.InnerException.Message}");
            }
        }
        else
        {

            try
            {
                var result2 = _db.Relations.Where(r => r.Follower == CurrentUser && r.Followed == ProfileUser).FirstOrDefault();
                _db.Relations.Remove(result2);
                _db.SaveChanges();
                _f.Flash(Types.Warning, $"Пользователь {CurrentUser.Name} отписался от {ProfileUser.Name}!");
            }
            catch (Exception ex)
            {
                _f.Flash(Types.Success, $"{ex.Message}");
            }


        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int messageid)
    {
        var sessionId = HttpContext.Session.GetString("SampleSession");
        CurrentUser = await _db.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(sessionId));

        try
        {
            Micropost m = _db.Microposts.Find(messageid);
            _db.Microposts.Remove(m);
            _db.SaveChanges();
            _log.Log(LogLevel.Error, $"Удалено сообщение \"{m.Content}\" пользователя {CurrentUser.Name}!");
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _log.Log(LogLevel.Error, $"Ошибка удаления сообщения: {ex.InnerException}");
            _log.Log(LogLevel.Error, $"Модель привязки из маршрута: {messageid}");
        }

        return Page();

    }

}
