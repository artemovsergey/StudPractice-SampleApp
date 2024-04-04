using System;
using System.Collections.Generic;

namespace SampleApp.Domen.Models;

public partial class Micropost
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User? User { get; set; }
}
