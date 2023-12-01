using System;
using System.Collections.Generic;

namespace SampleApp.RazorPage.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PasswordConfirmation { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual ICollection<Micropost> Microposts { get; set; } = new List<Micropost>();

    public virtual ICollection<Relation> RelationFolloweds { get; set; } = new List<Relation>();

    public virtual ICollection<Relation> RelationFollowers { get; set; } = new List<Relation>();
}
