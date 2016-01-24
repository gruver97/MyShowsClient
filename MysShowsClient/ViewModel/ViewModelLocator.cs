/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MysShowsClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using MysShowsClient.Model.Parser;
using MysShowsClient.Services.MyShow;
using MysShowsClient.Views;

namespace MysShowsClient.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IUnityContainer _unityContainer = new UnityContainer();

        public ViewModelLocator()
        {
            var navigationService = new NavigationService();
            var descriptionPageType = typeof(FullDescriptionPage);
            navigationService.Configure(descriptionPageType.Name, descriptionPageType);
            _unityContainer.RegisterType<IParser, Parser>()
                .RegisterType<IMyShowService, MyShowService>()
                .RegisterType<ISearchViewModel, SearchViewModel>()
                .RegisterType<IFullDescriptionViewModel, FullDescriptionViewModel>()
                .RegisterInstance(navigationService);
        }

        public ISearchViewModel SearchViewModel => _unityContainer.Resolve<ISearchViewModel>();
        public IFullDescriptionViewModel FullDescriptionViewModel => _unityContainer.Resolve<IFullDescriptionViewModel>();
    }
}