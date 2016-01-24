using System.Collections.Generic;
using System.Threading.Tasks;

namespace MysShowsClient.Model.Parser
{
    public interface IParser
    {
        Task<IEnumerable<ShortDescription>> DeserializeShortDescriptionAsync(string jsonString);
        Task<ExtendedDescription> DeserializeExtendedDescriptionAsync(string jsonString);
    }
}