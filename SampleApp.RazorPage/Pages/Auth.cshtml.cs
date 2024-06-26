using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.Domen.Models;

namespace SampleApp.RazorPage.Pages;

public class AuthModel : PageModel
{

    public User User { get; set; }

    private readonly HttpClient _http;
    private readonly ILogger<AuthModel> _log;
    private readonly IFlasher _f;
    private IHttpClientFactory factory;
    public AuthModel(HttpClient http, ILogger<AuthModel> log, IFlasher f, IHttpClientFactory factory)
    {
        _http = factory.CreateClient("API");
        _log = log;
        _f = f;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(User user)
    {

        var response = await _http.GetAsync($"{_http.BaseAddress}/users/auth?email={user.Email}&password={user.Password}");

        var current_user = await response.Content.ReadFromJsonAsync<User>();

        if (response.IsSuccessStatusCode)
        {
            HttpContext.Session.SetString("SampleSession", $"{current_user.Id}");
            _f.Flash(Types.Primary, $"����� ����������, {current_user.Name}!");
            return RedirectToPage("Index");
        }
        else
        {
            _f.Flash(Types.Danger, $"�������� ����� ��� ������!");
            return Page();
        }
    }

    public IActionResult OnGetLogout()
    {
        // ����� ������
        HttpContext.Session.Clear();
        return RedirectToPage("Auth");
    }

}
