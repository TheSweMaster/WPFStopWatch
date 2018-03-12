using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfStopWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private string _stopWatch;
        public string StopWatch { get { return _stopWatch; } set { _stopWatch = value; OnPropertyChanged(); } }

        private string _currenttime;
        public string CurrentTime { get { return _currenttime; } set { _currenttime = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Stopwatch stopWatch = new Stopwatch();
        private StringBuilder stringBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            CurrentTime = DateTime.Now.ToLongTimeString();
            StopWatch = "00:00:00.00";

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromMilliseconds(10),
                IsEnabled = true,
            };
            timer.Tick += (s, e) =>
            {
                CurrentTime = DateTime.Now.ToLongTimeString();
                StopWatch = stopWatch.Elapsed.ToString("hh\\:mm\\:ss\\.ff");
            };
        }

        private void Timer_Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning == false)
            {
                stopWatch.Start();
            }
        }

        private void Timer_Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning == true)
            {
                stopWatch.Stop();
            }
        }

        private void Timer_Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Reset();
        }

    }
}
