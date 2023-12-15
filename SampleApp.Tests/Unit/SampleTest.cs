using Microsoft.AspNetCore.Mvc.Testing;
using SampleApp.RazorPage;
using System.Net;

namespace SampleApp.Tests.Unit
{
    public class SampleTest
    {

        #region Конструктор
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public SampleTest()
        {
            // Arrange

            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }
        #endregion

        [Fact]
        public async Task HomePage_ReturnsSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/Sign");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task HelpPage_ReturnsSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/Auth");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}