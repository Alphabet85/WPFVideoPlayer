using System.ComponentModel;

namespace WPFVideoPlayer.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region " INotifyPropertyChanged "

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
