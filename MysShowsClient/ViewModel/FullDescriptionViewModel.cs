using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using MysShowsClient.Model;
using MysShowsClient.Services.MyShow;

namespace MysShowsClient.ViewModel
{
    public class FullDescriptionViewModel : ViewModelBase, IFullDescriptionViewModel
    {
        private readonly IMyShowService _showService;
        public ExtendedDescription Description { get; set; }

        public FullDescriptionViewModel([Dependency] IMyShowService showService)
        {
            _showService = showService;
        }
    }
}