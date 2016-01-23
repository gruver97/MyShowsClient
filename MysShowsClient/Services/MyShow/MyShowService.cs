using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MysShowsClient.Model;

namespace MysShowsClient.Services.MyShow
{
    public class MyShowService : IMyShowService
    {
        public async Task<List<Description>> SearchShowsAsync(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<ExtendedDescription> GetShowDescriptionAsync(int showId)
        {
            throw new NotImplementedException();
        }
    }
}