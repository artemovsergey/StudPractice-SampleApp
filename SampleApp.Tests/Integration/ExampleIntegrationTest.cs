using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Tests.Integration
{
    public class StatusMiddlewareTests
    {
        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                webHost.Configure(app => app.UseMiddleware<StatusMiddleware>());
                webHost.UseTestServer();
            });
            IHost host = await hostBuilder.StartAsync();
            HttpClient client = host.GetTestClient();
            var response = await client.GetAsync("/ping");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("pong", content);
        }
    }
}
