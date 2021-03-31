using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFVideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VideoTime_Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            MainWindowViewModel mwvm = (MainWindowViewModel)DataContext;
            mwvm.Pause();
        }

        private void VideoTime_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MainWindowViewModel mwvm = (MainWindowViewModel)DataContext;
            mwvm.Play();
        }
    }
}
