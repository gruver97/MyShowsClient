﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using MysShowsClient.EventArguments;
using MysShowsClient.Model;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    public class SearchViewModel : ViewModelBase, ISearchViewModel
    {
        private readonly IMyShowService _myShowService;
        private string _searchQuery;

        public SearchViewModel([Dependency] IMyShowService myShowService)
        {
            _myShowService = myShowService;
            ShortDescriptions = new ObservableCollection<ShortDescription>();
            SearchCommand = DelegateCommand.FromAsyncHandler(() => SearchShowAsync(SearchQuery),
                () => !string.IsNullOrWhiteSpace(SearchQuery));
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
        public event EventHandler<ChangeVisualStateEventArgs> VisualStateChanged;

        private async Task SearchShowAsync(string searchQuery)
        {
            try
            {
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.LoadingState));
                var result = await _myShowService.SearchShowsAsync(searchQuery);
                if (result != null && result.Item2 == null)
                {
                    ShortDescriptions.Clear();
                    foreach (var shortDescription in result.Item1)
                    {
                        if (string.IsNullOrWhiteSpace(shortDescription.Image)) shortDescription.Image = "ms-appx:///Assets/no_image.png";
                        ShortDescriptions.Add(shortDescription);
                    }
                }
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.LoadedState));
            }
            catch (Exception)
            {
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.ErrorState));
            }
        }

        protected virtual void OnVisualStateChanged(ChangeVisualStateEventArgs e)
        {
            VisualStateChanged?.Invoke(this, e);
        }
    }
}