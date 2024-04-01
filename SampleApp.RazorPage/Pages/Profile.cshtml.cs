using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;
using System.Text.Json;

namespace SampleApp.RazorPage.Pages;

public class ProfileModel : PageModel
{

    private readonly SampleAppContext _db;
    private readonly ILogger<ProfileModel> _log;
    private readonly IFlasher _f;
    private HttpClient _http;

    public User ProfileUser { get; set; }
    public User CurrentUser { get; set; }

    public bool IsFollow { get; set; }

    public ProfileModel(SampleAppContext db, ILogger<ProfileModel> log, IFlasher f, IHttpClientFactory factory)
    {
        _db = db;
        _log = log;
        _f = f;
        _http = factory.CreateClient("API");
    }

    public async Task<IActionResult> OnGetAsync([FromRoute] int? id)
    {
        var sessionId = HttpContext.Session.GetString("SampleSession");

        var response = await _http.GetAsync($"{_http.BaseAddress}/Users/{id}");

        if (response.IsSuccessStatusCode)
        {
            ProfileUser = await response.Content.ReadFromJsonAsync<User>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        var responseCurrentUser = await _http.GetAsync($"{_http.BaseAddress}/Users/{sessionId}");

        if (responseCurrentUser.IsSuccessStatusCode)
        {
            CurrentUser = await responseCurrentUser.Content.ReadFromJsonAsync<User>();
        }

        var responseRelation = await _http.GetAsync($"{_http.BaseAddress}/Users/Find?followerId={CurrentUser.Id}&followedId={ProfileUser.Id}");

        if (responseRelation.IsSuccessStatusCode)
        {
            IsFollow = await responseRelation.Content.ReadFromJsonAsync<bool>();
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


}
