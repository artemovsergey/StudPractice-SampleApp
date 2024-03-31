using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.Domen.Models;

namespace SampleApp.RazorPage.Pages;

public class SignModel : PageModel
{
    private readonly HttpClient _http;
    private readonly ILogger<SignModel> _log;
    private readonly IFlasher _f;
    public SignModel(HttpClient http, ILogger<SignModel> log, IFlasher f)
    {
        _http = http;
        _log = log;
        _f = f;
    }

    public void OnGet()
    {
        Console.WriteLine("����������� �������� Sign");
    }

    public async Task<IActionResult> OnPost(User user)
    {

        //if (!user.IsUniqEmail())
        //{
        //    ModelState.AddModelError("UniqEmail","����� ���� ����� ��� ���� � ���� ������!");
        //}

        //if (!ModelState.IsValid)
        //{
        //    _log.LogInformation($"�� ���������� ������ ������!");
        //    _f.Flash(Types.Danger, $"�� ���������� ������ ������!", dismissable: false);
        //    return Page();
        //}

       
        try
        {
            var response = await _http.PostAsJsonAsync<User>("https://localhost:7225/api/users", user);
            if (response.IsSuccessStatusCode)
            {
                _log.LogInformation($"������������ {user.Name} ������� ���������������!");
                _f.Flash(Types.Primary, $"������������  {user.Name} ���������������!", dismissable: false);
                return RedirectToPage("./Index");
            }
            else
            {
                _log.LogError($"������ ���: {response.StatusCode}");
                return Page();
            }

        }
        catch (Exception ex)
        {
            _log.LogError($"������: {ex.InnerException.Message}");
            _f.Flash(Types.Danger, $"������ �����������: {ex.InnerException.Message}", dismissable: false);
            return RedirectToPage("./Sign");
        }
    }



}
