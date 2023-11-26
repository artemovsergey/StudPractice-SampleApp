namespace SampleApp.RazorPage.Models
{
    public class Micropost
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // при миграции EF создаст UserId
        public User User { get; set; }
    }
}
