using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Com.RePower.Ocv.Ui.YiWei.Views.UserControls
{
    /// <summary>
    /// AlarmBox.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmBox : UserControl
    {
        System.Timers.Timer timerAlert = new System.Timers.Timer();
        private Brush StopColor = new SolidColorBrush(Colors.LightGray);

        private Brush RunningColorLight = new SolidColorBrush(Colors.LightSkyBlue);
        private Brush RunningColorDark = new SolidColorBrush(Colors.DodgerBlue);

        private Brush WarningColor = new SolidColorBrush(Colors.Orange);
        private Brush ErrorColor = new SolidColorBrush(Colors.OrangeRed);
        private Brush FatalColor = new SolidColorBrush(Colors.Red);

        bool _isInterval = true;

        private AlarmType _alarmType = AlarmType.Stop;


        public AlarmBox()
        {
            InitializeComponent();
            timerAlert.Elapsed += TimerAlert_Elapsed;
            Draw(_alarmType);
        }

        [System.ComponentModel.Description("指示灯类别")]
        public AlarmType AlarmType
        {
            get => _alarmType;
            set
            {
                if (_alarmType != value)
                    Draw(value);
            }
        }



        public AlarmType BindAlarmType
        {
            get { return (AlarmType)GetValue(BindAlarmTypeProperty); }
            set { SetValue(BindAlarmTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindAlarmType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindAlarmTypeProperty =
            DependencyProperty.Register("BindAlarmType", typeof(AlarmType), typeof(AlarmBox), new PropertyMetadata(AlarmType.Stop,BindAlarmTypeChanged));

        private static void BindAlarmTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AlarmBox source = (AlarmBox)d;
            var value = (AlarmType)e.NewValue;
            if (source.AlarmType != value)
            {
                source.Draw(value);
            }
        }

        private void Draw(AlarmType value)
        {
            this._alarmType = value;

            switch (value)
            {
                case AlarmType.Stop:
                    timerAlert.Stop();
                    Stop();
                    break;
                case AlarmType.Start:
                    //timerAlert.Interval = 500;  //正常工作
                    //timerAlert.Start();
                    timerAlert.Stop();
                    Start();
                    break;
                case AlarmType.Warning:
                    timerAlert.Interval = 400;  //毫秒
                    timerAlert.Start();
                    break;
                case AlarmType.Error:
                    timerAlert.Interval = 200;  //毫秒
                    timerAlert.Start();
                    break;
                case AlarmType.Fatal:
                    timerAlert.Interval = 100;  //毫秒
                    timerAlert.Start();
                    break;
                default:
                    break;
            }
        }
        private void Stop()
        {
            //Application.Current.Dispatcher.Invoke(new Action(() =>
            //{
            //    Dot.Background = StopColor;
            //}), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Dot.Background = StopColor;
            });
        }


        private void TimerAlert_Elapsed(object? sender, ElapsedEventArgs e)
        {
            switch (_alarmType)
            {
                case AlarmType.Stop:
                    Stop();
                    break;
                case AlarmType.Start:
                    Start();
                    break;
                case AlarmType.Warning:
                    Warning();
                    break;
                case AlarmType.Error:
                    Error();
                    break;
                case AlarmType.Fatal:
                    Fatal();
                    break;
                default:
                    break;
            }
        }

        private void Fatal()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_isInterval)
                {
                    Dot.Background = FatalColor;
                }
                else
                {
                    Dot.Background = StopColor;
                }
                _isInterval = !_isInterval;
            });
        }

        private void Error()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                if (_isInterval)
                {
                    Dot.Background = ErrorColor;
                }
                else
                {
                    Dot.Background = StopColor;
                }
                _isInterval = !_isInterval;
            });
        }

        private void Warning()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_isInterval)
                {
                    Dot.Background = WarningColor;
                }
                else
                {
                    Dot.Background = StopColor;
                }
                _isInterval = !_isInterval;
            });
        }

        private void Start()
        {
            Application.Current.Dispatcher.Invoke(() =>Dot.Background = RunningColorLight);
        }
    }

    public enum AlarmType
    {
        Stop = 0,
        Start = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
    }
}
