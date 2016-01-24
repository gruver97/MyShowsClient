using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using MysShowsClient.Model;

namespace MysShowsClient.ViewModel
{
    public interface ISearchViewModel
    {
        string SearchQuery { get; set; }
        ObservableCollection<ShortDescription> ShortDescriptions { get; }
        RelayCommand SearchCommand { get; } 
    }
}