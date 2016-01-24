using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Unity;
using MysShowsClient.Model;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    public class SearchViewModel : ViewModelBase, ISearchViewModel
    {
        private readonly IMyShowService _myShowService;
        private string _searchQuery;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (_searchQuery == value) return;
                _searchQuery = value;
                RaisePropertyChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<ShortDescription> ShortDescriptions { get; }
        public RelayCommand SearchCommand { get; }

        public SearchViewModel([Dependency] IMyShowService myShowService)
        {
            _myShowService = myShowService;
            ShortDescriptions = new ObservableCollection<ShortDescription>();
            SearchCommand = new RelayCommand(() => { }, ()=>!string.IsNullOrWhiteSpace(SearchQuery));
        }
    }
}