using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApplication.Service;
using Flurl.Http.Testing;
using Shouldly;
using Xunit;

namespace ConsoleApplication.Tests.Tests
{
    public class MyServiceTests
    {
        [Fact]
        public async Task DoAsyncTest()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWith("Internal Server Error", 500);
            var service = new MyService();
            
            await Should.ThrowAsync<Exception>(async () =>
            {
                var isSuccessStatusCode = await service.DoAsync();
                isSuccessStatusCode.ShouldBeTrue();
            });
        }

        [Fact]
        public async Task DoAsyncTest02()
        {
            using var httpTest = new HttpTest();
            httpTest.RespondWith("OK", 200);
            var service = new MyService();
            
            var isSuccessStatusCode = await service.DoAsync();

            isSuccessStatusCode.ShouldBeTrue();
            httpTest.ShouldHaveCalled("http://www.google.com")
                .WithVerb(HttpMethod.Get);
        }
    }
}
