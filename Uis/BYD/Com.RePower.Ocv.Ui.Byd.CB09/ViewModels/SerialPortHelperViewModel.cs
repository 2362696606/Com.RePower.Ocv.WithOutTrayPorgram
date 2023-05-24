using Com.RePower.Device.Ohm;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    public class SerialPortHelperViewModel:ObservableObject
    {
        public IOhm Ohm { get; set; }
        public SerialPortHelperViewModel()
        {
            this.Ohm = IocHelper.Default.GetService<IOhm>() ?? throw new Exception("内阻仪实例为null");
            Reflash();
            this.ReflashCommand = new RelayCommand(Reflash);
            this.ConnectCommand = new RelayCommand(DoConnect);
        }
        private string _portName;

        public string PortName
        {
            get { return _portName; }
            set { SetProperty(ref _portName, value); }
        }
        private int _baudRate;

        public int BaudRate
        {
            get { return _baudRate; }
            set { SetProperty(ref _baudRate, value); }
        }
        private int _realDelay;

        public int ReadDelay
        {
            get { return _realDelay; }
            set { SetProperty(ref _realDelay, value); }
        }

        private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }

        public RelayCommand ReflashCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand HaxSendCommand { get; set; }
        public RelayCommand StringSendCommand { get; set; }
        private bool _isNeedRecovery;

        public bool IsNeedRecovery
        {
            get { return _isNeedRecovery; }
            set { SetProperty(ref _isNeedRecovery, value); }
        }

        private string _sendCmd = string.Empty;

        public string SendCmd
        {
            get { return _sendCmd; }
            set { SetProperty(ref _sendCmd, value); }
        }
        private string _recoveryCmd;

        public string RecoveryCmd
        {
            get { return _recoveryCmd; }
            set { SetProperty(ref _recoveryCmd, value); }
        }

        private ObservableCollection<string> _recording;

        public ObservableCollection<string> Recording
        {
            get { return _recording; }
            set { SetProperty(ref _recording, value); }
        }



        public void Reflash()
        {
            if (Ohm is IOhmSerialPort sdb)
            {
                this.PortName = sdb.PortName;
                this.BaudRate = sdb.BaudRate;
                this.IsConnected = sdb.IsConnected;
                //this.ReadDelay = sdb.ReadDelay;
                if (Ohm is HiokiBt3562Impl hio)
                {
                    this.ReadDelay = hio.ReadDelay;
                }
                else
                    this.ReadDelay = 0;
            }
            else
            {
                this.PortName = string.Empty;
                this.BaudRate = 0;
                this.IsConnected = false;
                this.ReadDelay = 0;
            }
        }
        public void DoConnect()
        {
            var result = Ohm.Connect();
            Reflash();
        }
        public void HaxSend()
        {
            byte[] byteArray = new byte[] { 1, 2, 3 };

            
        }
    }
}
