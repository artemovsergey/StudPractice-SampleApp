namespace SampleApp.Domen.Application;

public static class UserExtension
{
    public static bool IsPasswordConfirmation(this Domen.Models.User user)
    {
        return (user.Password == user.PasswordConfirmation) ? true : false;
    }

    public static bool IsUniqEmail(this Domen.Models.User user)
    {
        //using SampleAppContext db = new ();
        //var u = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
        //return u != null ? false : true;

        return true;
    }

}
