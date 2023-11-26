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

        public UsersModel(SampleApp.RazorPage.Models.SampleContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
