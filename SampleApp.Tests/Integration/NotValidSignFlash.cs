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
    public class NotValidSignFlash
    {
        [Fact]
        public async Task OnPost_NotValidData_ShouldViewNotValidFlash()
        {

        }
    }
}
