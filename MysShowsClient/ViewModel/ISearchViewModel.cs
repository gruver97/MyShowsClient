using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Prism.Commands;
using MysShowsClient.EventArguments;
using MysShowsClient.Model;

namespace MysShowsClient.ViewModel
{
    public interface ISearchViewModel
    {
        string SearchQuery { get; set; }
        ObservableCollection<ShortDescription> ShortDescriptions { get; }
        DelegateCommand SearchCommand { get; }
        event EventHandler<ChangeVisualStateEventArgs> VisualStateChanged;
        string InfoMessage { get; }
        DelegateCommand<ItemClickEventArgs> NavigateToDetailsPageCommand { get; }
    }
}