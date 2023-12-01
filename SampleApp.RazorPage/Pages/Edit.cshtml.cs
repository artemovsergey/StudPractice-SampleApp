using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Pages
{
    public class EditModel : PageModel
    {
        private readonly SampleContext _context;
        private readonly IFlasher _f;
        private readonly ILogger<EditModel> _log;

        public EditModel(SampleContext context, IFlasher f, ILogger<EditModel> log)
        {
            _context = context;
            _f = f;
            _log = log;
        }

        [BindProperty]
        public User Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Input = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Input.Password = Input.HashPassword(Input.Password);
            Input.PasswordConfirmation = Input.HashPassword(Input.PasswordConfirmation);
            _context.Attach(Input).State = EntityState.Modified;

            try
            {
              await _context.SaveChangesAsync();
              _f.Flash(Types.Success, $"Пользователь {Input.Name} обновлен!", dismissable: true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Input.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
