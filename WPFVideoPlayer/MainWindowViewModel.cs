using System.Windows.Input;

using WPFVideoPlayer.Helpers;
using WPFVideoPlayer.ViewModels;

namespace WPFVideoPlayer
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region " Constructor " 

        public MainWindowViewModel()
        {
        }

        #endregion

        #region " Properties "

        private object _runningViewModel;
        public object RunningViewModel
        {
            get { return _runningViewModel; }
            set 
            { 
                _runningViewModel = value;
                OnPropertyChanged("RunningViewModel");
            }
        }

        private string _buttonContent = "Open Video Player";
        public string ButtonContent
        {
            get { return _buttonContent; }
            set 
            { 
                _buttonContent = value;
                OnPropertyChanged("ButtonContent");
            }
        }

        private bool _isVideoPlayerOpen;
        public bool IsVideoPlayerOpen
        {
            get { return _isVideoPlayerOpen; }
            set
            { 
                _isVideoPlayerOpen = value;

                if (_isVideoPlayerOpen)
                {
                    ButtonContent = "Close Video Player";
                }
                else
                {
                    ButtonContent = "Open Video Player";
                }
            }
        }

        #endregion

        #region " Methods "

        #endregion

        #region " Command Methods "

        private void ToggleVideoPlayer()
        {
            IsVideoPlayerOpen = !IsVideoPlayerOpen;

            if (IsVideoPlayerOpen)
            {
                Mouse.OverrideCursor = Cursors.Wait;

                RunningViewModel = new VideoPlayerViewModel();

                Mouse.OverrideCursor = Cursors.Arrow;
            }
            else
            {
                if (RunningViewModel != null)
                {
                    RunningViewModel = null;
                }
            }
        }

        #endregion

        #region " Commands "

        private ICommand _toggleVideoPlayerCommand;
        public ICommand ToggleVideoPlayerCommand
        {
            get
            {
                if (_toggleVideoPlayerCommand == null)
                {
                    _toggleVideoPlayerCommand = new RelayCommand(P => true, p => ToggleVideoPlayer());
                }

                return _toggleVideoPlayerCommand;
            }
        }

        #endregion
    }
}
