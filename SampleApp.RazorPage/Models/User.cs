using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace SampleApp.RazorPage.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password {  get; set; } = null!;
        public string PasswordConfirmation { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;

        public virtual List<Relation> Relations { get; set; }
        public List<Micropost> Microposts { get; set; }


        public bool IsFollow(User user, User u)
        {
            using (SampleContext db = new SampleContext())
            {
                Relation r = db.Relation.Where(r => r.UserId == user.Id && r.FollowedUserId == u.Id).FirstOrDefault() as Relation;
                return (r != null) ? true : false;
            }

        }

    }

}

