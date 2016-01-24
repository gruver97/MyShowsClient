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

using Microsoft.Practices.Unity;
using MysShowsClient.Model.Parser;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private IUnityContainer _unityContainer = new UnityContainer();

        public ViewModelLocator()
        {
            _unityContainer.RegisterType<IParser, Parser>()
                .RegisterType<IMyShowService, MyShowService>()
                .RegisterType<ISearchViewModel, SearchViewModel>();
        }

        public ISearchViewModel SearchViewModel => _unityContainer.Resolve<ISearchViewModel>();
    }
}