using Core.Flash;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;

namespace SampleApp.RazorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private IFlasher _f;
    private SampleAppContext _db;
    private HttpClient _http;
    public IndexModel(IHttpClientFactory factory,ILogger<IndexModel> logger, IFlasher f)
    {
        _http = factory.CreateClient("API");
        _logger = logger;
        _f = f;

    }


    public User? User { get; set; }
    public string sessionId { get; set; }
    public IEnumerable<User> Followeds { get; set; }
    public List<User> Users { get; set; } = new();
    public List<Micropost> Messages { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {

        sessionId = HttpContext.Session.GetString("SampleSession");

        if (sessionId == null)
        {
            return RedirectToPage("Auth");
        }
        else
        {


            Followeds = await _http.GetFromJsonAsync<IEnumerable<User>>($"{_http.BaseAddress}/Users/{sessionId}/followeds");

            User = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{sessionId}");

            //Followeds = User.RelationFollowers.Select(item => item.Followed).ToList();w

            Users.AddRange(Followeds);
            Users.Add(User);

            foreach (var u in Users)
            {
                //_db.Entry(u).Collection(u => u.Microposts).Load();

                var messages = await _http.GetFromJsonAsync<IEnumerable<Micropost>>($"{_http.BaseAddress}/Users/{u.Id}/messages");

                Messages.AddRange(messages);
            }

            return Page();
        }

    }

    public async Task<IActionResult> OnPostAsync(string message)
    {
        sessionId = HttpContext.Session.GetString("SampleSession");

        User = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{sessionId}");
        
        if (!string.IsNullOrWhiteSpace(message))
        {
            var m = new Micropost()
            {
                Content = message,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = this.User.Id,
            };


            try
            {
                var response = await _http.PostAsJsonAsync<Micropost>($"{_http.BaseAddress}/Microposts", m);
                _f.Flash(Types.Primary, $"Tweet!");
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Ошибка создания сообщения: {ex.InnerException}");

                return Page();
            }



        }
        else
        {
            return Page();
        }

    }

    public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
    {
        sessionId = HttpContext.Session.GetString("SampleSession");

        User = await _http.GetFromJsonAsync<User>($"{_http.BaseAddress}/Users/{sessionId}");
        
        try
        {
            //var message = await _http.GetFromJsonAsync<Micropost>($"{_http.BaseAddress}/Microposts/FindMessage?messageId={id}");

            await _http.DeleteAsync($"{_http.BaseAddress}/Microposts/{id}");
            
            _logger.Log(LogLevel.Error, $"Удалено сообщение пользователя {User.Name}!");
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