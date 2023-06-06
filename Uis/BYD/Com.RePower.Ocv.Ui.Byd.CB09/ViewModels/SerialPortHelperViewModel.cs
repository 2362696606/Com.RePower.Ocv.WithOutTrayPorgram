using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
            this.HexSendCommand = new RelayCommand(HexSend);
            this.StringSendCommand = new RelayCommand(StringSend);
        }
        private string _portName = string.Empty;

        public string PortName
        {
            get => _portName;
            set => SetProperty(ref _portName, value);
        }
        private int _baudRate;

        public int BaudRate
        {
            get => _baudRate;
            set => SetProperty(ref _baudRate, value);
        }
        private int _realDelay;

        public int ReadDelay
        {
            get => _realDelay;
            set => SetProperty(ref _realDelay, value);
        }

        private bool _isConnected;

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public RelayCommand ReflashCommand { get; set; }
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand HexSendCommand { get; set; }
        public RelayCommand StringSendCommand { get; set; }
        private bool _isNeedRecovery;

        public bool IsNeedRecovery
        {
            get => _isNeedRecovery;
            set => SetProperty(ref _isNeedRecovery, value);
        }

        private string _sendCmd = string.Empty;

        public string SendCmd
        {
            get => _sendCmd;
            set => SetProperty(ref _sendCmd, value);
        }
        private string _recoveryCmd = string.Empty;

        public string RecoveryCmd
        {
            get => _recoveryCmd;
            set => SetProperty(ref _recoveryCmd, value);
        }

        private ObservableCollection<string> _recording = new ObservableCollection<string>();

        public ObservableCollection<string> Recording
        {
            get => _recording;
            set => SetProperty(ref _recording, value);
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
            if (!Ohm.IsConnected)
            {
                Ohm.Connect();
            }
            else
            {
                Ohm.DisConnect();
            }
            Reflash();
        }
        public void HexSend()
        {
            var cmd = HexStringToByteArray(SendCmd);
            Recording.Add($"发：{Convert.ToHexString(cmd)}");
            var result = Ohm.SendCmd(cmd, isNeedRecovery: IsNeedRecovery);
            if (result is { IsSuccess: true, Content.Length: > 0 })
            {
                Recording.Add($"收：{Convert.ToHexString(result.Content)}");
                this.RecoveryCmd = Convert.ToHexString(result.Content);
            }
            Reflash();
        }
        public void StringSend()
        {
            var cmd = Encoding.ASCII.GetBytes(SendCmd);
            Recording.Add($"发：{Convert.ToHexString(cmd)}");
            var result = Ohm.SendCmd(cmd, isNeedRecovery: IsNeedRecovery);
            if (result is { IsSuccess: true, Content.Length: > 0 })
            {
                Recording.Add($"收：{Convert.ToHexString(result.Content)}");
                this.RecoveryCmd = Convert.ToHexString(result.Content);
            }
            Reflash();
        }
        private byte[] HexStringToByteArray(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return new byte[] { };
            }
            string[] hexValuesSplit = hexString.Split(' ');
            List<byte> byteList = new List<byte>();
            foreach (var s in hexValuesSplit)
            {
                var value = Convert.ToByte(s, 16);
                byteList.Add(value);
            }
            return byteList.ToArray();
        }
    }
}
