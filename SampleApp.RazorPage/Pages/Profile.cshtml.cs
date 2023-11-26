using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApp.RazorPage.Application;
using SampleApp.RazorPage.Models;

namespace SampleApp.RazorPage.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly SampleApp.RazorPage.Models.SampleContext _context;

        public ProfileModel(SampleApp.RazorPage.Models.SampleContext context)
        {
            _context = context;
            
        }

        public User User { get; set; }


        public bool IsLogin()
        {
            return HttpContext.Session.GetString("SampleSession") != null ? true : false;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
               
                return NotFound();
            }

            User = await _context.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
