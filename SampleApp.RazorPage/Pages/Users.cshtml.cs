using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.Domen.Models;

namespace SampleApp.RazorPage.Pages;

public class UsersModel : PageModel
{

    private readonly SampleAppContext _db;
    private readonly ILogger<UsersModel> _log;
    private readonly IFlasher _f;
    private readonly HttpClient _http;

    public IEnumerable<User> Users { get; set; } = new List<User>();

    public UsersModel(IHttpClientFactory factory, ILogger<UsersModel> log, IFlasher f)
    {
        _http = factory.CreateClient("API");
        _log = log;
        _f = f;
    }

    public async Task OnGet()
    {
        var response = await _http.GetAsync($"{_http.BaseAddress}/Users");
        Users = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
    }

    public async Task<IActionResult> OnGetRemoveUser([FromQuery] int Id)
    {
        var response = await _http.DeleteAsync($"{_http.BaseAddress}/Users/{Id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Users");
        }
        else
        {
            _log.LogInformation("Не удается удалить пользователя!");
            return RedirectToPage();
        }
    }

}
