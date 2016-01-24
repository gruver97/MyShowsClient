using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MysShowsClient.EventArguments;
using MysShowsClient.Model;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    public class FullDescriptionViewModel : ShowViewModelBase, IFullDescriptionViewModel
    {
        private new const string NotFoundMessage = "Мы не смогли ничего найти, нам жаль(";
        private readonly IMyShowService _showService;
        private ExtendedDescription _description;

        public FullDescriptionViewModel([Dependency] IMyShowService showService)
        {
            _showService = showService;
        }

        public ExtendedDescription Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadShowDescriptionAsync(int showId)
        {
            try
            {
                OnVisualStateChanged(new ChangeVisualStateEventArgs(LoadingStatesEnum.LoadingState));
                var fullDescription = await _showService.GetShowDescriptionAsync(showId);
                if (fullDescription.Item1 != null && fullDescription.Item2 == null)
                {
                    foreach (var episode in fullDescription.Item1.Episodes.Where(episode => episode.Image == null))
                    {
                        episode.Image = "ms-appx:///Assets/no_image.png";
                    }
                    Description = fullDescription.Item1;
                }
                if (fullDescription.Item2 != null)
                {
                    if (fullDescription.Item2.IsSearchError)
                    {
                        InfoMessage = NotFoundMessage;
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