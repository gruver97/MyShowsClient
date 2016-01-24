using System;
using GalaSoft.MvvmLight;
using MysShowsClient.EventArguments;

namespace MysShowsClient.ViewModel
{
    public class ShowViewModelBase : ViewModelBase
    {
        protected const string NotFoundMessage = "Мы не смогли найти сериал по запросу \"{0}\"";
        protected const string ErrorMessage = "Упс, проблемка :-( ";
        private string _infoMessage;

        public string InfoMessage
        {
            get { return _infoMessage; }
            protected set
            {
                if (_infoMessage == value) return;
                _infoMessage = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler<ChangeVisualStateEventArgs> VisualStateChanged;

        protected virtual void OnVisualStateChanged(ChangeVisualStateEventArgs e)
        {
            VisualStateChanged?.Invoke(this, e);
        }
    }
}