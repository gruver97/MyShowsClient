﻿using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MysShowsClient.Common;
using MysShowsClient.EventArguments;
using MysShowsClient.ViewModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MysShowsClient.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FullDescriptionPage : Page
    {
        private int _requestedShowId;
        private StatusBar _statusBar;

        public FullDescriptionPage()
        {
            InitializeComponent();

            NavigationHelper = new NavigationHelper(this);
            NavigationHelper.LoadState += NavigationHelper_LoadState;
            NavigationHelper.SaveState += NavigationHelper_SaveState;
        }

        /// <summary>
        ///     Gets the <see cref="NavigationHelper" /> associated with this <see cref="Page" />.
        /// </summary>
        public NavigationHelper NavigationHelper { get; }

        /// <summary>
        ///     Gets the view model for this <see cref="Page" />.
        ///     This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel { get; } = new ObservableDictionary();

        public IFullDescriptionViewModel FullDescriptionViewModel { get; set; }

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        ///     The source of the event; typically <see cref="NavigationHelper" />
        /// </param>
        /// <param name="e">
        ///     Event data that provides both the navigation parameter passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested and
        ///     a dictionary of state preserved by this page during an earlier
        ///     session.  The state will be null the first time a page is visited.
        /// </param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        ///     Preserves state associated with this page in case the application is suspended or the
        ///     page is discarded from the navigation cache.  Values must conform to the serialization
        ///     requirements of <see cref="SuspensionManager.SessionState" />.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper" /></param>
        /// <param name="e">
        ///     Event data that provides an empty dictionary to be populated with
        ///     serializable state.
        /// </param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void FullDescriptionPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _statusBar = StatusBar.GetForCurrentView();
            FullDescriptionViewModel = DataContext as IFullDescriptionViewModel;
            FullDescriptionViewModel.VisualStateChanged += FullDescriptionViewModel_VisualStateChanged;
            FullDescriptionViewModel?.LoadShowDescriptionAsync(_requestedShowId);
        }

        private async void FullDescriptionViewModel_VisualStateChanged(object sender, ChangeVisualStateEventArgs e)
        {
            switch (e.LoadingStatesEnum)
            {
                case LoadingStatesEnum.None:
                    break;
                case LoadingStatesEnum.LoadingState:
                {
                    await _statusBar.ProgressIndicator.ShowAsync();
                    break;
                }
                case LoadingStatesEnum.LoadedState:
                    {
                        await _statusBar.ProgressIndicator.HideAsync();
                        break;
                    }
                case LoadingStatesEnum.ErrorState:
                    {
                        _statusBar.ProgressIndicator.Text = FullDescriptionViewModel.InfoMessage;
                        _statusBar.ProgressIndicator.ProgressValue = null;
                        await _statusBar.ProgressIndicator.ShowAsync();
                        break;
                    }
                case LoadingStatesEnum.NotFoundState:
                    {
                        _statusBar.ProgressIndicator.Text = FullDescriptionViewModel.InfoMessage;
                        _statusBar.ProgressIndicator.ProgressValue = null;
                        await _statusBar.ProgressIndicator.ShowAsync();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region NavigationHelper registration

        /// <summary>
        ///     The methods provided in this section are simply used to allow
        ///     NavigationHelper to respond to the page's navigation methods.
        ///     <para>
        ///         Page specific logic should be placed in event handlers for the
        ///         <see cref="NavigationHelper.LoadState" />
        ///         and <see cref="NavigationHelper.SaveState" />.
        ///         The navigation parameter is available in the LoadState method
        ///         in addition to page state preserved during an earlier session.
        ///     </para>
        /// </summary>
        /// <param name="e">
        ///     Provides data for navigation methods and event
        ///     handlers that cannot cancel the navigation request.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _requestedShowId = Convert.ToInt32(e.Parameter);
            NavigationHelper.OnNavigatedTo(e);
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            await _statusBar.HideAsync();
            NavigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}