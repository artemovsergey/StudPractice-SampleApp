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
    public class ValidSign
    {
        [Fact]
        public async Task OnPost_ValidData_ShouldReturnPageResult()
        {
            // Arrange

            var pageModel = new SignModel(new SampleContext(), new Mock<IFlasher>().Object, new Mock<ILogger<SignModel>>().Object)
            {
                PageContext = new PageContext { HttpContext = new DefaultHttpContext() }
            };

            // Act
            var user = new User() { Name = "user", Email = "user@valid", Password = "foo", PasswordConfirmation = "foo" };
            var result = pageModel.OnPost(user);

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("./Auth", redirectToPageResult.PageName);
        }
    }
}
