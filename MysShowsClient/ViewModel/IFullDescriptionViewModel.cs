using System;
using System.Threading.Tasks;
using MysShowsClient.EventArguments;
using MysShowsClient.Model;

namespace MysShowsClient.ViewModel
{
    public interface IFullDescriptionViewModel
    {
        ExtendedDescription Description { get; set; }
        Task LoadShowDescriptionAsync(int showId);
        event EventHandler<ChangeVisualStateEventArgs> VisualStateChanged;
        string InfoMessage { get; }
    }
}