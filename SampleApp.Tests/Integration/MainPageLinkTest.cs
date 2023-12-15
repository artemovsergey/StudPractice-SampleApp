using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.RazorPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

using AngleSharp;
using AngleSharp.Html.Parser;

namespace SampleApp.Tests.Integration
{
    public class MainPageLinksTest : IClassFixture<WebApplicationFactory<Program>>
    {

        #region Конструктор
        private readonly WebApplicationFactory<Program> _factory;

        public MainPageLinksTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        #endregion

        [Fact]
        public async Task MainPageLinks()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            // Parse the HTML response
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(await response.Content.ReadAsStringAsync());

            // Check that the links are present
            var signLink = document.QuerySelector("a[href='/Sign']");
            var authLink = document.QuerySelector("a[href='/Auth']");


            Assert.NotNull(signLink);
            Assert.NotNull(authLink);
        }
    }
}
