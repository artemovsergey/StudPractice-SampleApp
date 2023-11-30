using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [Required(ErrorMessage = "Требуется имя")]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password {  get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }


        [ValidateNever]
        public bool IsAdmin { get; set; } = false;
        
        [ValidateNever]
        public virtual List<Relation> Relations { get; set; }

        [ValidateNever]
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

