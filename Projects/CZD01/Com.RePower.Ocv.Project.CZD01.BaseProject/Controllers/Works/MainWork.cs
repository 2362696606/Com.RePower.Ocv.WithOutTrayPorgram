using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork : MainWorkAbstract
    {
        public MainWork(Tray tray,DevicesController devicesController)
        {
            Tray = tray;
            DevicesController = devicesController;
        }

        public SettingManager SettingManager
        {
            get { return SettingManager.Instance; }
        }
        public Tray Tray { get; }
        public DevicesController DevicesController { get; }

        protected override OperateResult DoWork()
        {
            while(true)
            {
                DoPauseOrStop();
                var connectResult = ConnectDevices();
                if (connectResult.IsFailed)
                    return connectResult;
                DoPauseOrStop();
                var testPreParationResult = TestPreparation();
                if (testPreParationResult.IsFailed)
                    return testPreParationResult;
                DoPauseOrStop();
                var getTrayCodeResult = GetTrayCode();
                if (getTrayCodeResult.IsFailed)
                    return getTrayCodeResult;
                DoPauseOrStop();
                var getBatteriesInfoResult = GetBatteriesInfo();
                if (getBatteriesInfoResult.IsFailed)
                    return getBatteriesInfoResult;
                DoPauseOrStop();
                var testBatteriesResult = TestBatteries();
                if (testBatteriesResult.IsFailed)
                    return testBatteriesResult;
                DoPauseOrStop();
                var verfyKValueResult = VerifyKValue();
                if (verfyKValueResult.IsFailed)
                    return verfyKValueResult;
                DoPauseOrStop();
                var saveToLocationResult = SaveToLocation();
                if (saveToLocationResult.IsFailed)
                    return saveToLocationResult;
                DoPauseOrStop();
                var saveToSceneDbResult = SaveToSceneDb();
                if (saveToSceneDbResult.IsFailed)
                    return saveToSceneDbResult;
                DoPauseOrStop();
                var uploadToMesResult = UploadTestResultToMes();
                if (uploadToMesResult.IsFailed)
                    return uploadToMesResult;
                DoPauseOrStop();
                var uploadToWmsResult = UploadTestResultToWms();
                if (uploadToWmsResult.IsFailed)
                    return uploadToWmsResult;
                DoPauseOrStop();
                var requestAllLocateCellResult = RequestAllLocateCellToWms();
                if (requestAllLocateCellResult.IsFailed)
                    return requestAllLocateCellResult;
                DoPauseOrStop();
                var unBindingResult = UnBindingTray();
                if (unBindingResult.IsFailed)
                    return unBindingResult;
                DoPauseOrStop();
                //var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)2);
                //if (writeResult.IsFailed)
                //    return writeResult;
            }
        }
    }
}
