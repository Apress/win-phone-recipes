using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace CustomersManager.Model
{
    [Table]
    public class Customer : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }


        [Column]
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [Column]
        private string _telephoneNumber;

        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set
            {
                NotifyPropertyChanging("TelephoneNumber");
                _telephoneNumber = value;
                NotifyPropertyChanged("TelephoneNumber");
            }
        }

        [Column]
        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                NotifyPropertyChanging("Address");
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion
    }
}