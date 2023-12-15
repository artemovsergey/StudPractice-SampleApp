using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.RazorPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests.Unit
{
    public class TitlePageTest
    {

        #region Конструктор
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public TitlePageTest()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }
        #endregion

        [Fact]
        public async Task Get_Home_ReturnsSuccess_WithCorrectTitle()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/Sign");

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
            var title = WebUtility.HtmlDecode(titleNode.InnerHtml);
            Assert.NotNull(titleNode);
            Assert.Equal("Регистрация", title);
        }

        [Fact]
        public async Task Get_Help_ReturnsSuccess_WithCorrectTitle()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/Auth");

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
            var title = WebUtility.HtmlDecode(titleNode.InnerHtml);
            Assert.NotNull(titleNode);
            Assert.Equal("Авторизация", title);
        }

        [Fact]
        public async Task Get_About_ReturnsSuccess_WithCorrectTitle()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/About");

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
            var title = WebUtility.HtmlDecode(titleNode.InnerHtml);
            Assert.NotNull(titleNode);
            Assert.Equal("About", title);
        }
    }
}
