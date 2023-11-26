using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using SampleApp.RazorPage.Pages;

namespace SampleApp.RazorPage.Application
{
    public static class ProfileModelExtensions
    {
        public static bool IsLogin(this ProfileModel model)
        {
            return model.HttpContext.Session.GetString("SampleSession") != null ? true : false;
        }
    }
}
