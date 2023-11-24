namespace SampleApp.RazorPage.Application
{
    public static class UserExtension
    {
        public static bool IsPasswordConfirmation(this Models.User user)
        {
            return (user.Password == user.PasswordConfirmation) ? true : false;
        }


    }
}
