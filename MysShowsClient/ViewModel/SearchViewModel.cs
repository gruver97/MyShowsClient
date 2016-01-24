using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using MysShowsClient.EventArguments;
using MysShowsClient.Model;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    public class SearchViewModel : ShowViewModelBase, ISearchViewModel
    {
        private readonly IMyShowService _myShowService;
        private readonly NavigationService _navigationService;
        private string _searchQuery;

        public SearchViewModel([Dependency] IMyShowService myShowService,
            [Dependency] NavigationService navigationService)
        {
            _myShowService = myShowService;
            _navigationService = navigationService;
            ShortDescriptions = new ObservableCollection<ShortDescription>();
            SearchCommand = DelegateCommand.FromAsyncHandler(() => SearchShowAsync(SearchQuery),
                () => !string.IsNullOrWhiteSpace(SearchQuery));
            NavigateToDetailsPageCommand =
                new DelegateCommand<ItemClickEventArgs>(
                    eventArgs =>
                    {
                        _navigationService.NavigateTo("FullDescriptionPage",
                            (eventArgs.ClickedItem as ShortDescription)?.Id);
                    });
        }

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
        public DelegateCommand SearchCommand { get; }

        public DelegateCommand<ItemClickEventArgs> NavigateToDetailsPageCommand { get; }

        private async Task SearchShowAsync(string searchQuery)
        {
            try
            {
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.LoadingState));
                var result = await _myShowService.SearchShowsAsync(searchQuery);
                ShortDescriptions.Clear();
                if (result != null && result.Item2 == null)
                {
                    foreach (var shortDescription in result.Item1)
                    {
                        if (string.IsNullOrWhiteSpace(shortDescription.Image))
                            shortDescription.Image = "ms-appx:///Assets/no_image.png";
                        ShortDescriptions.Add(shortDescription);
                    }
                    OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.LoadedState));
                }
                if (result.Item2 != null)
                {
                    if (result.Item2.IsSearchError)
                    {
                        InfoMessage = string.Format(NotFoundMessage, searchQuery);
                        OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.NotFoundState));
                    }
                    else
                    {
                        InfoMessage = ErrorMessage;
                        OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.ErrorState));
                    }
                }
            }
            catch (Exception)
            {
                InfoMessage = ErrorMessage;
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.ErrorState));
            }
        }
    }
}