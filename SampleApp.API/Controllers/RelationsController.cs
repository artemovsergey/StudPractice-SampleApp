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
public class RelationsController : ControllerBase
{
    private readonly SampleAppContext _context;

    public RelationsController(SampleAppContext context)
    {
        _context = context;
    }

    // GET /api/Relations/Find?followeId=a&followedId=b

    [HttpGet("Find")]
    public async Task<ActionResult<bool>> FindRelation(int followerId, int followedId)
    {
        // если текущий пользователь подписан на профиль пользователя
        var result = _context.Relations.Where(r => r.FollowerId == followerId && r.FollowedId == followedId).FirstOrDefault();

        return Ok(result != null ? true : false);

    }

    [HttpGet("FindRelation")]
    public async Task<ActionResult<Relation>> FindRelationById(int followerId, int followedId)
    {
        var relation = _context.Relations.Where(r => r.FollowerId == followerId && r.FollowedId == followedId).FirstOrDefault();

        if(relation == null)
        {
            return BadRequest("Такой связи нет!");
        }

        return Ok(relation);

    }


    // GET: api/Relations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Relation>>> GetRelations()
    {
      if (_context.Relations == null)
      {
          return NotFound();
      }
        return await _context.Relations.ToListAsync();
    }

    // GET: api/Relations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Relation>> GetRelation(int id)
    {
      if (_context.Relations == null)
      {
          return NotFound();
      }
        var relation = await _context.Relations.FindAsync(id);

        if (relation == null)
        {
            return NotFound();
        }

        return relation;
    }

    // PUT: api/Relations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRelation(int id, Relation relation)
    {
        if (id != relation.Id)
        {
            return BadRequest();
        }

        _context.Entry(relation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RelationExists(id))
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

    // POST: api/Relations
    [HttpPost]
    public async Task<ActionResult<Relation>> PostRelation(Relation r)
    {

        try
        {
            _context.Relations.Add(r);
            _context.SaveChanges();
            return CreatedAtAction("GetRelation", new { id = r.Id }, r);
        }
        catch (Exception ex)
        {
            return BadRequest($"Неверные значения id: {ex.InnerException.Message}");
        }
    }

    // DELETE: api/Relations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRelation(int id)
    {
        if (_context.Relations == null)
        {
            return NotFound();
        }
        var relation = await _context.Relations.FindAsync(id);
        if (relation == null)
        {
            return NotFound();
        }

        _context.Relations.Remove(relation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RelationExists(int id)
    {
        return (_context.Relations?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
