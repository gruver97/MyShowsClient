using System.Collections.Generic;
using System.Threading.Tasks;
using MysShowsClient.Model;

namespace MysShowsClient.Services.MyShow
{
    public interface IMyShowService
    {
        Task<List<Description>> SearchShowsAsync(string searchQuery);
        Task<ExtendedDescription> GetShowDescriptionAsync(int showId);
    }
}