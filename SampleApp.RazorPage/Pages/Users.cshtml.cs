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
        private readonly SampleApp.RazorPage.Models.SampleContext _context;
        private readonly ILogger<UsersModel> _log;
        
        public UsersModel(SampleApp.RazorPage.Models.SampleContext context, ILogger<UsersModel> log)
        {
            _context = context;
            _log = log;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
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
