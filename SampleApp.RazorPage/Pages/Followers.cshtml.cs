using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;

namespace SampleApp.RazorPage.Pages;

public class FollowersModel : PageModel
{
   
    public User ProfileUser { get; set; }
    public IEnumerable<User> Followers { get; set; }

    private HttpClient _http;

    public FollowersModel(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }


    public async Task OnGet([FromRoute] int id)
    {

        ProfileUser = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{id}"); 

        Followers = await _http.GetFromJsonAsync<IEnumerable<User>>($"{_http.BaseAddress}/Users/{id}/followers");

    }
}
