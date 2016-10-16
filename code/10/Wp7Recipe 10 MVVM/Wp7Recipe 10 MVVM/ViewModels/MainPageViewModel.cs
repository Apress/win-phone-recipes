using GalaSoft.MvvmLight;
using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace Wp7Recipe_10_2_MVVM.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        public enum States : uint { Saved, Unsaved};

        /// <summary>
        /// The name of our application
        /// </summary>
        public string ApplicationName { get { return "Learning MVVM"; } }

        /// <summary>
        /// The name of the page
        /// </summary>
        public string PageName { get { return "Main Page"; } }

        public GalaSoft.MvvmLight.Command.RelayCommand SaveCommand { get; set; }

        #region DateProperty

        /// <summary>
        /// The <see cref="Date" /> property's name.
        /// </summary>
        public const string DatePropertyName = "Date";

        private DateTime _date = DateTime.Now.AddDays(-4);

        /// <summary>
        /// Gets the Date property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                if (_date == value)
                {
                    return;
                }

                var oldValue = _date;
                _date = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(DatePropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(DatePropertyName, oldValue, value, true);
            }
        } 
        #endregion

        #region Amount Property
        /// <summary>
        /// The <see cref="Amount" /> property's name.
        /// </summary>
        public const string AmountPropertyName = "Amount";

        private decimal _amount = 0;

        /// <summary>
        /// Gets the Amount property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public decimal  Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                if (_amount == value)
                {
                    return;
                }

                var oldValue = _amount;
                _amount = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(AmountPropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(AmountPropertyName, oldValue, value, true);
            }
        }
        #endregion

        #region Motivation Property

        /// <summary>
        /// The <see cref="Motivation" /> property's name.
        /// </summary>
        public const string MotivationPropertyName = "Motivation";

        private string _motivation = string.Empty;

        /// <summary>
        /// Gets the Motivation property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Motivation
        {
            get
            {
                return _motivation;
            }

            set
            {
                if (_motivation == value)
                {
                    return;
                }

                var oldValue = _motivation;
                _motivation = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(MotivationPropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(MotivationPropertyName, oldValue, value, true);
            }
        }

        #endregion

        #region State Property

        /// <summary>
        /// The <see cref="State" /> property's name.
        /// </summary>
        public const string StatePropertyName = "State";

        private States _state = States.Unsaved;

        /// <summary>
        /// Gets the State property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public States State
        {
            get
            {
                return _state;
            }

            set
            {
                if (_state == value)
                {
                    return;
                }

                var oldValue = _state;
                _state = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(StatePropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(StatePropertyName, oldValue, value, true);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(SaveCommandExecute);
        }

        private void SaveCommandExecute()
        {
            State = (this.Amount == 0 || string.IsNullOrEmpty(Motivation))
                ? States.Unsaved : States.Saved;
        }

        private bool SaveCommandCanExecute()
        {
            return (Amount != 0 && !string.IsNullOrEmpty(Motivation));
        }
        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}