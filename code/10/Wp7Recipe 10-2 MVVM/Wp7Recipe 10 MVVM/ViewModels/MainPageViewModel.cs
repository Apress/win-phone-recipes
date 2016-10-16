using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Wp7Recipe_10_2_MVVM.Model;
using Wp7Recipe_10_2_MVVM.Repositories;

namespace Wp7Recipe_10_2_MVVM.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        public MainPageViewModel()
        {
            CloseNewsCommand = new DelegateCommand<News>(CloseNewsCommandExecute, CloseNewsCommandCanExecute);

        }

        #region Bindable Properties

        #region News
        private ObservableCollection<News> _news;

        public ObservableCollection<News> News
        {
            get { return _news ?? (_news = NewsRepository.GetNews()); }
        }
        #endregion

        #region IsDetailVisible
        private bool _isDetailVisible;

        public bool IsDetailVisibile
        {
            get { return _isDetailVisible; }
            set
            {
                _isDetailVisible = value;
                RaisePropertyChanged(() => IsDetailVisibile);
            }
        }

        #endregion

        #region SelectedNews
        private News _selectedNews;

        public News SelectedNews
        {
            get { return _selectedNews; }
            set
            {
                
                _selectedNews = value;
                RaisePropertyChanged(() => SelectedNews);
                IsDetailVisibile = true;
                CloseNewsCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #endregion

        #region Commands

        #region CloseNewsCommand
        public DelegateCommand<News> CloseNewsCommand { get; set; }

        private void CloseNewsCommandExecute(News news)
        {
            SelectedNews = null;
            IsDetailVisibile = false;
        }

        private bool CloseNewsCommandCanExecute(News news)
        {
            return IsDetailVisibile;
        }
        #endregion

        #endregion

    }
}