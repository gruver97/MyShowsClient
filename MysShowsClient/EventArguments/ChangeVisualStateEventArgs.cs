using System;
using MysShowsClient.ViewModel;

namespace MysShowsClient.EventArguments
{
    public class ChangeVisualStateEventArgs:EventArgs
    {
        public LoadingStatesEnum LoadingStatesEnum { get; set; }

        public ChangeVisualStateEventArgs(LoadingStatesEnum loadingStatesEnum)
        {
            LoadingStatesEnum = loadingStatesEnum;
        }
    }
}
