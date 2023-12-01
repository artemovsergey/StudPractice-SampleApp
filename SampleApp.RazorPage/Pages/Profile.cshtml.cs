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

        private readonly SampleContext _context;
        private readonly ILogger<ProfileModel> _logger;
        public ProfileModel(SampleContext context, ILogger<ProfileModel> logger)
        {
            _context = context;
            _logger = logger;
        }


        public User ProfileUser { get; set; }
        public bool IsFollow { get; set; }

        public async Task<IActionResult> OnGetAsync([FromRoute]int? id)
        {
            ProfileUser = await _context.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == id) as User;   
            return Page();
        }

    }
}
