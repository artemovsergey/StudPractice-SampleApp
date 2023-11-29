using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Pages
{
    public class UsersModel : PageModel
    {
        private readonly SampleContext _context;
        private readonly ILogger<UsersModel> _log;
        
        public UsersModel(SampleContext context, ILogger<UsersModel> log)
        {
            _context = context;
            _log = log;
        }

        public IList<User> Users { get;set; }

        public User User { get; set; }

        public string sessionId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            sessionId = HttpContext.Session.GetString("SampleSession");
            Users = await _context.Users.ToListAsync();

            if (sessionId != null)
            {
                User = await _context.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(sessionId));
                return Page();
            }
            else
            {
                return RedirectToPage("Auth");
            }

        }


        public async Task<IActionResult> OnGetRemoveAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToPage();
            }
            catch(Exception ex)
            {
                _log.LogError($"{ex.Message}");        
            }
            return Page();
        }



    }
}
