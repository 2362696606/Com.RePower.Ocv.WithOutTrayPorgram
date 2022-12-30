using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Crypto.Modes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels.Windows
{
    public partial class DevicesManagerViewModel:ObservableObject
    {
        private SerialPortHelper serialPort;
        public DevicesManagerViewModel()
        {
            this.serialPort = new SerialPortHelper();
            this.serialPort.RecoveryModel = RecoveryModel.Manual;
            this.serialPort.ReadDelay = 1000;
            this.serialPort.PortName = "COM4";
            this.serialPort.BaudRate = 9600;
            this.serialPort.Open();
        }
        [ObservableProperty]
        private string readResult = string.Empty;
        [ObservableProperty]
        private string parseResult = string.Empty;
        [RelayCommand]
        private void ReadRes()
        {
            byte[] cmd = Encoding.ASCII.GetBytes(@":READ?" + "\r\n");
            var result = this.serialPort.SendAndRecovery(cmd,10000);
            if(result.IsSuccess)
            {
                this.ReadResult = result.Content!.ToHexString(',');
                this.ParseResult = Encoding.ASCII.GetString(result.Content!);
            }
        }
    }
}
