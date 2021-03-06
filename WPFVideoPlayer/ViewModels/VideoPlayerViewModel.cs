using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

using WPFVideoPlayer.Helpers;
using WPFVideoPlayer.Models;

namespace WPFVideoPlayer.ViewModels
{
    public class VideoPlayerViewModel : ViewModelBase
    {
        #region " Constructor " 

        public VideoPlayerViewModel()
        {
            VolumeLevel = 1;

            Stop();
            GetVideoCollectionFromSource();
        }

        #endregion

        #region " Properties "

        private DispatcherTimer _timer;

        private MediaElement _mediaElementObject;
        public MediaElement MediaElementObject
        {
            get { return _mediaElementObject; }
            set
            {
                _mediaElementObject = value;
                OnPropertyChanged("MediaElementObject");
            }
        }

        private string _mediaLocation;
        public string MediaLocation
        {
            get { return _mediaLocation; }
            set
            {
                _mediaLocation = value;
                OnPropertyChanged("MediaLocation");
            }
        }

        private double _volumeLevel;
        public double VolumeLevel
        {
            get { return _volumeLevel; }
            set
            {
                _volumeLevel = value;

                if (MediaElementObject != null)
                {
                    MediaElementObject.Volume = VolumeLevel;
                }

                OnPropertyChanged("VolumeLevel");
            }
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                _isMuted = value;

                if (MediaElementObject != null)
                {
                    MediaElementObject.IsMuted = IsMuted;
                }

                OnPropertyChanged("IsMuted");
            }
        }

        private double _sliderValue;
        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                _sliderValue = value;

                if (MediaElementObject != null && MediaElementObject.HasVideo)
                {
                    MediaElementObject.Position = TimeSpan.FromSeconds(_sliderValue);
                }

                OnPropertyChanged("SliderValue");
            }
        }

        private double _sliderMaximum;
        public double SliderMaximum
        {
            get { return _sliderMaximum; }
            set
            {
                _sliderMaximum = value;
                OnPropertyChanged("SliderMaximum");
            }
        }

        private bool _isVideoPaused;
        public bool IsVideoPaused
        {
            get { return _isVideoPaused; }
            set
            {
                _isVideoPaused = value;
                OnPropertyChanged("IsVideoPaused");
            }
        }

        private bool _isMediaLoaded;
        public bool IsMediaLoaded
        {
            get { return _isMediaLoaded; }
            set
            {
                _isMediaLoaded = value;

                if (_isMediaLoaded)
                {
                    VideoStatusText = "Press Play to Start";
                }
                else
                {
                    VideoStatusText = "Video Not Loaded\nPlease Select a Video";
                }

                OnPropertyChanged("IsMediaLoaded");
            }
        }

        private string _videoStatusText;
        public string VideoStatusText
        {
            get { return _videoStatusText; }
            set
            {
                _videoStatusText = value;
                OnPropertyChanged("VideoStatusText");
            }
        }

        private string _videoTimeRemainingText;
        public string VideoTimeRemainingText
        {
            get { return _videoTimeRemainingText; }
            set
            {
                _videoTimeRemainingText = value;
                OnPropertyChanged("VideoTimeRemainingText");
            }
        }

        private ObservableCollection<VideoModel> _videoModelCollection;
        public ObservableCollection<VideoModel> VideoModelCollection
        {
            get { return _videoModelCollection; }
            set
            {
                _videoModelCollection = value;
                OnPropertyChanged("VideoModelCollection");
            }
        }

        private VideoModel _selectedVideo;
        public VideoModel SelectedVideo
        {
            get
            {
                if (_selectedVideo != null)
                {
                    LoadAndPlayMediaElement(_selectedVideo);
                }

                return _selectedVideo;
            }
            set
            {
                _selectedVideo = value;
                OnPropertyChanged("SelectedVideo");
            }
        }

        private bool _isSettingsOpen;
        public bool IsSettingsOpen
        {
            get { return _isSettingsOpen; }
            set 
            { 
                _isSettingsOpen = value;
                OnPropertyChanged("IsSettingsOpen");
            }
        }

        private string _selectedSourceLocation;
        public string SelectedSourceLocation
        {
            get { return _selectedSourceLocation; }
            set
            {
                _selectedSourceLocation = value;
                OnPropertyChanged("SelectedSourceLocation");
            }
        }

        #endregion

        #region " Methods "

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MediaElementObject != null)
                {
                    if ((MediaElementObject.Source != null) && (MediaElementObject.NaturalDuration.HasTimeSpan))
                    {
                        SliderMaximum = MediaElementObject.NaturalDuration.TimeSpan.TotalSeconds;
                        SliderValue = MediaElementObject.Position.TotalSeconds;
                        VideoTimeRemainingText = MediaElementObject.Position.ToString(@"mm\:ss") + " / " + MediaElementObject.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timer_Tick() Exception: " + ex);
            }
        }

        private void GetVideoCollectionFromSource()
        {
            try
            {
                VideoModelCollection = new ObservableCollection<VideoModel>();

                foreach (string item in Directory.GetFiles(Properties.Settings.Default.VideosSourceLocation, "*", SearchOption.AllDirectories))
                {
                    if (item.Contains(".mp4") || item.Contains(".mov") || item.Contains(".wmv") || item.Contains(".avi"))
                    {
                        VideoModelCollection.Add(new VideoModel(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetVideoCollectionFromSource() Exception: " + ex);
            }
        }

        private void LoadAndPlayMediaElement(VideoModel _videoModel)
        {
            try
            {
                MediaLocation = _videoModel.Location;

                MediaElementObject = new MediaElement
                {
                    Source = new Uri(_videoModel.Location),
                    LoadedBehavior = MediaState.Manual,
                    UnloadedBehavior = MediaState.Close
                };

                IsMediaLoaded = true;

                Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadAndPlayMediaElement() Exception: " + ex);
            }
        }

        #endregion

        #region " Command Methods "

        public void Play()
        {
            try
            {
                _timer = new DispatcherTimer();

                if (MediaElementObject != null)
                {
                    MediaElementObject.Play();

                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += Timer_Tick;
                    _timer.Start();

                    IsVideoPaused = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Play() Exception: " + ex);
            }
        }

        public void Pause()
        {
            try
            {
                if (MediaElementObject != null)
                {
                    MediaElementObject.Pause();
                    IsVideoPaused = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Pause() Exception: " + ex);
            }
        }

        public void Stop()
        {
            try
            {
                if (MediaElementObject != null)
                {
                    MediaElementObject.Stop();
                    MediaElementObject = null;

                    IsMediaLoaded = false;
                    IsVideoPaused = false;

                    _timer.Stop();
                    _timer = new DispatcherTimer();
                }

                SliderValue = 0;
                SliderMaximum = 100;
                VideoStatusText = "Video Not Loaded\nPlease Select a Video";
                VideoTimeRemainingText = "00:00 / 00:00";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Stop() Exception: " + ex);
            }
        }

        private void OpenSettings()
        {
            IsSettingsOpen = true;
            SelectedSourceLocation = Properties.Settings.Default.VideosSourceLocation;
        }

        private void BrowseSourceLocation()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Properties.Settings.Default.VideosSourceLocation;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedSourceLocation = dialog.SelectedPath;
            }
        }

        private void SettingsOk()
        {
            IsSettingsOpen = false; 

            Properties.Settings.Default.VideosSourceLocation = SelectedSourceLocation;
            Properties.Settings.Default.Save();

            VideoModelCollection = null;
            GetVideoCollectionFromSource();
        }

        private void SettingsCancel()
        {
            IsSettingsOpen = false;
        }

        #endregion

        #region " Commands "

        private ICommand _playCommand;
        public ICommand PlayCommand
        {
            get
            {
                if (_playCommand == null)
                {
                    _playCommand = new RelayCommand(P => true, p => Play());
                }

                return _playCommand;
            }
        }

        private ICommand _pauseCommand;
        public ICommand PauseCommand
        {
            get
            {
                if (_pauseCommand == null)
                {
                    _pauseCommand = new RelayCommand(P => true, p => Pause());
                }

                return _pauseCommand;
            }
        }

        private ICommand _stopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_stopCommand == null)
                {
                    _stopCommand = new RelayCommand(P => true, p => Stop());
                }

                return _stopCommand;
            }
        }

        private ICommand _openSettingsCommand;
        public ICommand OpenSettingsCommand
        {
            get
            {
                if (_openSettingsCommand == null)
                {
                    _openSettingsCommand = new RelayCommand(P => true, p => OpenSettings());
                }

                return _openSettingsCommand;
            }
        }

        private ICommand _browseSourceLocationCommand;
        public ICommand BrowseSourceLocationCommand
        {
            get
            {
                if (_browseSourceLocationCommand == null)
                {
                    _browseSourceLocationCommand = new RelayCommand(P => true, p => BrowseSourceLocation());
                }

                return _browseSourceLocationCommand;
            }
        }

        private ICommand _settingsOkCommand;
        public ICommand SettingsOkCommand
        {
            get
            {
                if (_settingsOkCommand == null)
                {
                    _settingsOkCommand = new RelayCommand(P => true, p => SettingsOk());
                }

                return _settingsOkCommand;
            }
        }

        private ICommand _settingsCancelCommand;
        public ICommand SettingsCancelCommand
        {
            get
            {
                if (_settingsCancelCommand == null)
                {
                    _settingsCancelCommand = new RelayCommand(P => true, p => SettingsCancel());
                }

                return _settingsCancelCommand;
            }
        }

        #endregion
    }
}
