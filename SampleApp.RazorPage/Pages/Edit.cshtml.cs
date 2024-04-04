using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleApp.Domen.Models;
using System.Net.Http.Json;
using System.Text;

namespace SampleApp.RazorPage.Pages;

public class EditModel : PageModel
{

    private readonly SampleAppContext _db;
    private readonly ILogger<EditModel> _log;
    private readonly IFlasher _f;
    private HttpClient _http;
    public EditModel(IHttpClientFactory factory, ILogger<EditModel> log, IFlasher f)
    {
        _http = factory.CreateClient("API");
        _log = log;
        _f = f;
    }

    [BindProperty]
    public User User { get; set; }

    public async Task OnGet()
    {
        var sessionId = Convert.ToInt32(HttpContext.Session.GetString("SampleSession"));

        
        User = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{sessionId}");

    }

    public async Task OnPost()
    {
        var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
        var response = await _http.PutAsJsonAsync<User>($"{_http.BaseAddress}/Users/{User.Id}", User);

        if (response.IsSuccessStatusCode)
        {
            _log.LogError($"Пользователь обновлен!");
            _f.Primary($"Пользователь обновлен!");
        }
        else
        {
            _log.LogError($"Ошибка обновления");
            _f.Danger($"Ошибка обновления");
        }
    }
}