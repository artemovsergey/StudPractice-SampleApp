namespace SampleApp.RazorPage.Models
{
    public class Relation
    {

        public int Id { get; set; }
        public int FollowedUserId { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}
