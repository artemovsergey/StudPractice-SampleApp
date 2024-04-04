using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;

namespace SampleApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly SampleAppContext _context;

    public UsersController(SampleAppContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        return await _context.Users.ToListAsync();
    }


    [HttpGet("{id}/followeds")]
    public async Task<ActionResult<IEnumerable<User>>> GetUserFolloweds([FromRoute] int id)
    {

        var relations = await _context.Relations.Where(r => r.FollowerId == id).ToListAsync();
        var followedsId = relations.Select(item => item.FollowedId).ToList();

        List<User> users = new List<User>();
        foreach (var i in followedsId)
        {
            var user = await _context.Users.Include(u => u.Microposts).Where(u => u.Id == i).FirstOrDefaultAsync();
            users.Add(user);
        }

        //var user = _context.Users
        //                         //.Include(u => u.RelationFollowers)
        //                         //.ThenInclude(r => r.Followed)
        //                         //.ThenInclude(u => u.Microposts)
        //                         //.ThenInclude(m => m.User)
        //                         .Where(u => u.Id == id)
        //                         .FirstOrDefault();

        return users;

    }




    [HttpGet("{id}/followers")]
    public async Task<ActionResult<IEnumerable<User>>> GetUserFollowers([FromRoute] int id)
    {


        var user = _context.Users.Include(u => u.RelationFolloweds)
                              .ThenInclude(r => r.Follower)
                              .Where(u => u.Id == id)
                              .FirstOrDefault();

        return user.RelationFolloweds.Select(item => item.Follower).ToList();

    }


    [HttpGet("{id}/messages")]
    public async Task<ActionResult<IEnumerable<Micropost>>> GetUserMicroposts([FromRoute] int id)
    {

        return await _context.Microposts.Include(m => m.User).Where(m => m.UserId == id).ToListAsync();
    }


    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var user = await _context.Users.Include(u => u.Microposts).ThenInclude(m => m.User).FirstOrDefaultAsync(m => m.Id == id) as User;

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        if (_context.Users == null)
        {
            return Problem("Entity set 'SampleAppContext.Users'  is null.");
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }


    // GET: api/Users/auth?email=a&password=b
    [HttpGet("auth")]
    public async Task<ActionResult<User>> GetByEmailAndPassword(string email, string password)
    {

        var current_user = _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        if (current_user != null)
        {
           return Ok(current_user);
        }
        else
        {
            return Problem("Не удается найти пользователя!");
        }
    }


    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
