using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleApp.Domen.Models;

using System.Diagnostics.CodeAnalysis;

namespace SampleApp.RazorPage.Pages
{
    public class UsersModel : PageModel
    {

        private readonly SampleAppContext _db;
        private readonly ILogger<UsersModel> _log;
        private readonly IFlasher _f;

        public IEnumerable<User> Users { get; set; } = new List<User>();

        public UsersModel(SampleAppContext db, ILogger<UsersModel> log, IFlasher f)
        {
            _db = db;
            _log = log;
            _f = f;
        }

        public void OnGet()
        {
           Users = _db.Users.ToList();
        }

        public IActionResult OnGetRemoveUser([FromQuery] int Id)
        {
            var user = _db.Users.Find(Id);
            if(user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToPage("Users");
        }

    }
}
