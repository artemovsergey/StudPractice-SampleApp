using Microsoft.AspNetCore.Mvc;
using SampleApp.Domen.Models;

namespace SampleApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MicropostsController : ControllerBase
{
    private readonly SampleAppContext _db;
    private readonly ILogger<MicropostsController> _log;

    public MicropostsController(SampleAppContext db, ILogger<MicropostsController> log)
    {
        _db = db;
        _log = log;
    }

    [HttpPost]
    public async Task<ActionResult> PostMicropost(Micropost m)
    {
        _db.Microposts.Add(m);

        try
        {
            _db.SaveChanges();
            _log.LogInformation("Сообщение добавлено в ленту!");
            return Ok();
        }
        catch (Exception ex)
        {
            _log.LogError($"{ex.InnerException.Message}");
            return BadRequest($"Ошибка добавления сообщения: {ex.InnerException.Message}");
        }
    }

    [HttpGet("FindMessage")]
    public async Task<ActionResult<Micropost>> GetMessage([FromQuery] int messageId)
    {
        var message = await _db.Microposts.FindAsync(messageId);
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Micropost>> DeleteMessage([FromRoute] int id)
    {

        var m = await _db.Microposts.FindAsync(id);
        _db.Microposts.Remove(m);

        try
        {
            await _db.SaveChangesAsync();
            _log.LogInformation("Операция удаления успешно завершена!");
            return Ok(m);
        }
        catch (Exception ex)
        {
            _log.LogError($"{ex.InnerException.Message}");
            return BadRequest();
        }

    }

}
