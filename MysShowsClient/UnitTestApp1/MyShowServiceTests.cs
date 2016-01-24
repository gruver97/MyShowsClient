using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MysShowsClient.Services.MyShow;

namespace UnitTestApp1
{
    [TestClass]
    public class MyShowServiceTests
    {
        [TestMethod]
        public async Task TestSearch()
        {
            var service = new MyShowService();
            var result = await service.SearchShowsAsync("theory");
            Assert.IsNull(result.Item2);
        }
    }

}
