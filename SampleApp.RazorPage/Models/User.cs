using SampleApp.RazorPage.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.RazorPage.Models;

public partial class User
{
    [ExampleValidation]
    public int Id { get; set; }


    [Required(ErrorMessage = "Требуется имя")]
    [StringLength(20, ErrorMessage = "Maximum length is {1}")]
    [Display(Name = "Имя пользователя")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Требуется почта")]
    [EmailAddress]
    [Display(Name = "Почта")]
    public string Email { get; set; }

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Пароль должен быть!")]
    public string Password { get; set; }

    public string PasswordConfirmation { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Micropost> Microposts { get; set; } = new List<Micropost>();

    public virtual ICollection<Relation> RelationFolloweds { get; set; } = new List<Relation>();

    public virtual ICollection<Relation> RelationFollowers { get; set; } = new List<Relation>();
}
