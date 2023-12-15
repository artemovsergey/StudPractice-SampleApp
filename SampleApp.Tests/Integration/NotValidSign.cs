using Core.Flash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SampleApp.RazorPage.Models;
using SampleApp.RazorPage.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests.Integration
{
    public class NotValidSign
    {
        [Fact]
        public async Task OnPost_InvalidData_ShouldReturnPageResult()
        {
            // Arrange

            var pageModel = new SignModel(new Mock<SampleContext>().Object, new Mock<IFlasher>().Object, new Mock<ILogger<SignModel>>().Object)
            {
                PageContext = new PageContext { HttpContext = new DefaultHttpContext() }
            };
            pageModel.ModelState.AddModelError("Name", "Name is required.");
            pageModel.ModelState.AddModelError("Email", "Email is required.");
            pageModel.ModelState.AddModelError("Password", "Password is required.");
            pageModel.ModelState.AddModelError("ConfirmPassword", "ConfirmPassword is required.");

            // Act
            var user = new User() { Name = "", Email = "user@invalid", Password = "foo", PasswordConfirmation = "bar" };
            var result = pageModel.OnPost(user);

            // Assert
            Assert.IsType<PageResult>(result);
        }


    }
}
