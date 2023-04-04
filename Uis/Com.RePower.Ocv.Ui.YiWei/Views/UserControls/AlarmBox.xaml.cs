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
        System.Timers.Timer _timerAlert = new System.Timers.Timer();
        private Brush _stopColor = new SolidColorBrush(Colors.LightGray);

        private Brush _runningColorLight = new SolidColorBrush(Colors.LightSkyBlue);
        private Brush _runningColorDark = new SolidColorBrush(Colors.DodgerBlue);

        private Brush _warningColor = new SolidColorBrush(Colors.Orange);
        private Brush _errorColor = new SolidColorBrush(Colors.OrangeRed);
        private Brush _fatalColor = new SolidColorBrush(Colors.Red);

        bool _isInterval = true;

        private AlarmType _alarmType = AlarmType.Stop;


        public AlarmBox()
        {
            InitializeComponent();
            _timerAlert.Elapsed += TimerAlert_Elapsed;
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
                    _timerAlert.Stop();
                    Stop();
                    break;
                case AlarmType.Start:
                    //timerAlert.Interval = 500;  //正常工作
                    //timerAlert.Start();
                    _timerAlert.Stop();
                    Start();
                    break;
                case AlarmType.Warning:
                    _timerAlert.Interval = 400;  //毫秒
                    _timerAlert.Start();
                    break;
                case AlarmType.Error:
                    _timerAlert.Interval = 200;  //毫秒
                    _timerAlert.Start();
                    break;
                case AlarmType.Fatal:
                    _timerAlert.Interval = 100;  //毫秒
                    _timerAlert.Start();
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
                Dot.Background = _stopColor;
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
                    Dot.Background = _fatalColor;
                }
                else
                {
                    Dot.Background = _stopColor;
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
                    Dot.Background = _errorColor;
                }
                else
                {
                    Dot.Background = _stopColor;
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
                    Dot.Background = _warningColor;
                }
                else
                {
                    Dot.Background = _stopColor;
                }
                _isInterval = !_isInterval;
            });
        }

        private void Start()
        {
            Application.Current.Dispatcher.Invoke(() =>Dot.Background = _runningColorLight);
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
