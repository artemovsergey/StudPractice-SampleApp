using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;
using System.Text.Json;

namespace SampleApp.RazorPage.Pages;

public class ProfileModel : PageModel
{

    private readonly ILogger<ProfileModel> _log;
    private readonly IFlasher _f;
    private HttpClient _http;

    public User ProfileUser { get; set; }
    public User CurrentUser { get; set; }

    public bool IsFollow { get; set; }

    public ProfileModel(ILogger<ProfileModel> log, IFlasher f, IHttpClientFactory factory)
    {
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

        var responseRelation = await _http.GetAsync($"{_http.BaseAddress}/Relations/Find?followerId={CurrentUser.Id}&followedId={ProfileUser.Id}");

        if (responseRelation.IsSuccessStatusCode)
        {
            IsFollow = await responseRelation.Content.ReadFromJsonAsync<bool>();
        }

        return Page();
    }


    public async Task<IActionResult> OnPostAsync([FromRoute] int? id)
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


        // если текущий пользователь подписан на профиль пользователя

        var responseRelation = await _http.GetAsync($"{_http.BaseAddress}/Relations/Find?followerId={CurrentUser.Id}&followedId={ProfileUser.Id}");

        if (responseRelation.IsSuccessStatusCode)
        {
            IsFollow = await responseRelation.Content.ReadFromJsonAsync<bool>();
        }


        if (IsFollow == false)
        {
            try
            {
                var relation = new Relation() { FollowerId = CurrentUser.Id, FollowedId = ProfileUser.Id };
                var responseFollow = await _http.PostAsJsonAsync($"{_http.BaseAddress}/Relations",relation);

                if (responseFollow.IsSuccessStatusCode)
                {
                    _f.Flash(Types.Success, $"Пользователь {CurrentUser.Name} подписался на {ProfileUser.Name}!");
                    IsFollow = true;
                }
                
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

                var responseFindRelation = await _http.GetAsync($"{_http.BaseAddress}/Relations/FindRelation?followerId={CurrentUser.Id}&followedId={ProfileUser.Id}");
                var currentRelation = await responseFindRelation.Content.ReadFromJsonAsync<Relation>();

                var responseDeleteRelation = await _http.DeleteAsync($"{_http.BaseAddress}/Relations/{currentRelation.Id}");

                _f.Flash(Types.Warning, $"Пользователь {CurrentUser.Name} отписался от {ProfileUser.Name}!");
                IsFollow = false;
            }
            catch (Exception ex)
            {
                _f.Flash(Types.Success, $"{ex.Message}");
            }


        }

        return RedirectToPage();
    }


}
