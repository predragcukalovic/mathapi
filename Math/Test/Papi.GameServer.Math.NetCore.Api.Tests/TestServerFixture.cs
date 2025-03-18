using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;


namespace Papi.GameServer.Math.NetCore.Api.Tests
{
    public class TestServerFixture : IDisposable
    {
        public TestServer TestServer { get; }
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
        }

        public void Dispose()
        {
            TestServer?.Dispose();
            Client?.Dispose();
        }
    }

    [CollectionDefinition("MathCollection")]
    public class MathCollection : ICollectionFixture<TestServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
