namespace SampleApp.RazorPage.Models
{
    public class Micropost
    {
        public int id { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
