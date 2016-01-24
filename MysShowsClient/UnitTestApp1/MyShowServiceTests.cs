using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MysShowsClient.Services.MyShow;

namespace UnitTestApp1
{
    [TestClass]
    public class MyShowServiceTests
    {
        [TestMethod]
        public async Task TestGeneralSearchAsync()
        {
            var service = new MyShowService();
            var result = await service.SearchShowsAsync("theory");
            Assert.IsNotNull(result.Item1);
            Assert.IsNull(result.Item2);
        }

        [TestMethod]
        public async Task TestEmptySearch()
        {
            var service = new MyShowService();
            try
            {
                await service.SearchShowsAsync(null);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof (ArgumentException));
            }
            try
            {
                await service.SearchShowsAsync(string.Empty);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof (ArgumentException));
            }
        }
    }
}