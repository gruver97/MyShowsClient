﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MysShowsClient.Model.Parser;
using MysShowsClient.Services.MyShow;

namespace UnitTestApp1
{
    [TestClass]
    public class MyShowServiceTests
    {
        [TestMethod]
        public async Task TestGeneralSearchAsync()
        {
            var service = new MyShowService(new Parser());
            var result = await service.SearchShowsAsync("theory");
            Assert.IsNotNull(result.Item1);
            Assert.IsNull(result.Item2);
        }

        [TestMethod]
        public async Task TestEmptySearch()
        {
            var service = new MyShowService(new Parser());
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

        [TestMethod]
        public async Task NotFoundTestAsync()
        {
            var service = new MyShowService(new Parser());
            //сервер возращает пустой массив вместо 404, поэтому считаю, что пустой массив равен ответу 404
            var searchResult = await service.SearchShowsAsync("adsfjas;dfjaskfjasdjfaksjfasjf");
            Assert.AreEqual(searchResult.Item1.Count(),0);
            Assert.IsTrue(searchResult.Item2.IsSearchError);
            Assert.AreEqual(searchResult.Item2.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task TestExtendedInfoAsync()
        {
            var service = new MyShowService(new Parser());
            var result = await service.GetShowDescriptionAsync(113);
            Assert.IsNotNull(result.Item1);
            Assert.IsNull(result.Item2);
        }

        [TestMethod]
        public async Task TestNotFoundEpisodesAsync()
        {
            var service = new MyShowService(new Parser());
            var result = await service.GetShowDescriptionAsync(-1);
            Assert.IsNull(result.Item1);
            Assert.IsTrue(result.Item2.IsSearchError);
            Assert.AreEqual(result.Item2.StatusCode, HttpStatusCode.NotFound);
        }
    }
}