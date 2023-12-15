using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.RazorPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests.Integration
{
    public class MainPageLinksWithSignTitleTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MainPageLinksWithSignTitleTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

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

            // Navigate to the Sign page and check the title
            response = await client.GetAsync("/Sign");
            parser = new HtmlParser();
            document = await parser.ParseDocumentAsync(await response.Content.ReadAsStringAsync());
            var title = document.QuerySelector("title").TextContent;

            Assert.Equal("Регистрация", title);
        }
    }
}
