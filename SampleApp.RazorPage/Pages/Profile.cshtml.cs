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
            if (id == null)
            {
               
                return NotFound();
            }

            ProfileUser = await _context.Users.Include(u => u.Microposts).FirstOrDefaultAsync(m => m.Id == id) as User;
            
            if (ProfileUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            try
            {
                //_context.Relation.Add(new Relation() { UserId = User.Id, FollowedUserId == })
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, $"{ex.Message}");
            }


            _logger.Log(LogLevel.Information, $"{IsFollow}");
            return RedirectToPage();
        }

    }
}
