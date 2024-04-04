using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;
using static System.Net.WebRequestMethods;

namespace SampleApp.RazorPage.Pages;

public class FollowedsModel : PageModel
{
 

    public User ProfileUser { get; set; }
    public IEnumerable<User> Followeds { get; set; }

    private HttpClient _http;

    public FollowedsModel(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
    }

    public async Task OnGet(int id)
    {
        ProfileUser = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{id}");
        Followeds = await _http.GetFromJsonAsync<IEnumerable<User>>($"{_http.BaseAddress}/Users/{id}/followeds");


    }
}
