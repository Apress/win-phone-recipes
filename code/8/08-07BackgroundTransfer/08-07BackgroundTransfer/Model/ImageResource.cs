using System;
using System.ComponentModel;

namespace _08_07BackgroundTransfer.Model
{
    public class ImageResource : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region ImageUri
        private Uri _imageUri;

        public Uri ImageUri
        {
            get { return _imageUri; }
            set
            {
                RaisePropertyChanging("ImageUri");
                _imageUri = value;
                RaisePopertyChanged("ImageUri");
            }
        } 
        #endregion

        #region Title
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                RaisePropertyChanging("Title");
                _title = value;
                RaisePopertyChanged("Title");
            }
        } 
        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePopertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }
        #endregion
    }
}