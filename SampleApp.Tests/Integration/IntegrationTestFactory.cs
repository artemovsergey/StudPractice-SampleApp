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
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _fixture;
        public IntegrationTests(
        WebApplicationFactory<Program> fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task PingRequest_ReturnsPong()
        {
            HttpClient client = _fixture.CreateClient();
            var response = await client.GetAsync("/ping");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("pong", content);
        }
    }
}
