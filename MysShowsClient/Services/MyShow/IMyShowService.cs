using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MysShowsClient.Model;

namespace MysShowsClient.Services.MyShow
{
    public interface IMyShowService
    {
        Task<Tuple<List<ShortDescription>, ErrorData>> SearchShowsAsync(string searchQuery);
        Task<ExtendedDescription> GetShowDescriptionAsync(int showId);
    }
}