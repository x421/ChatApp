using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using Chat;

using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

public class BasicTests
{
    private readonly HttpClient _factory;

    public BasicTests()
    {
        var server = new TestServer(new WebHostBuilder().UseEnvironment("Developer").UseStartup<Chat.Startup>());

        _factory = server.CreateClient();
    }

    [Fact]
    public void Test()
    {
        new TestServer(new WebHostBuilder().UseEnvironment("Developer").UseStartup<Chat.Startup>());
        Assert.Equal(1, 1);
    }
}

/*
using Xunit;

namespace MyFirstUnitTests
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
*/
