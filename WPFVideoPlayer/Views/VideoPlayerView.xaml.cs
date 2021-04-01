using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFVideoPlayer.ViewModels;

namespace WPFVideoPlayer.Views
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl
    {
        public VideoPlayerView()
        {
            InitializeComponent();
        }

        private void VideoTime_Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            VideoPlayerViewModel mwvm = (VideoPlayerViewModel)DataContext;
            mwvm.Pause();
        }

        private void VideoTime_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            VideoPlayerViewModel mwvm = (VideoPlayerViewModel)DataContext;
            mwvm.Play();
        }
    }
}
