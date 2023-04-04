using Autofac;
using Autofac.Extensions.DependencyInjection;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Ui.UiBase.ViewModels;
using Com.RePower.WpfBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Com.RePower.Ocv.Ui.UiBase
{
    public abstract class UiBaseApplication:Application
    {
        #region Windows API

        // ShowWindow 参数  
        public const int SwRestore = 9;

        /// <summary>
        /// 在桌面窗口列表中寻找与指定条件相符的第一个窗口。
        /// </summary>
        /// <param name="lpClassName">指向指定窗口的类名。如果 lpClassName 是 NULL，所有类名匹配。</param>
        /// <param name="lpWindowName">指向指定窗口名称(窗口的标题）。如果 lpWindowName 是 NULL，所有windows命名匹配。</param>
        /// <returns>返回指定窗口句柄</returns>
        [DllImport("USER32.DLL", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 将窗口还原,可从最小化还原
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 激活指定窗口
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
 
        #endregion



        public UiBaseApplication(bool isSingle = true,string? mainWindowsName = null)
        {
            this._isSingle = isSingle;
            this._mainWindowsName = mainWindowsName;
            this.Startup += new StartupEventHandler(App_Startup);
            Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri("/Com.RePower.Ocv.Ui.UiBase;component/Views/Dictionarys/BaseDictionary.xaml", UriKind.RelativeOrAbsolute) }
                );
        }

        /// <summary>
        /// 应用程序是否应为单例
        /// </summary>
        private bool _isSingle = true;
        /// <summary>
        /// 主窗口名
        /// </summary>
        private string? _mainWindowsName;
        Mutex? _mutex;

        protected virtual void App_SingleStart()
        {
            if(_isSingle)
            {
                _mutex = new Mutex(true, "OcvApplication", out bool isUnExists);
                if(!isUnExists)
                {
                    if(!string.IsNullOrEmpty(_mainWindowsName))
                    {
                        // 找到已经在运行的实例句柄(给出你的窗体标题名 “Deamon Club”)
                        IntPtr? hWndPtr = FindWindow(null!, _mainWindowsName);

                        if ((hWndPtr is { } tempPtr) && tempPtr != new IntPtr(0)) 
                        {
                            // 还原窗口
                            ShowWindow(tempPtr, SwRestore);

                            // 激活窗口
                            SetForegroundWindow(tempPtr);
                        }
                        else
                        {
                            MessageBox.Show("程序已经在运行", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("程序已经在运行", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    // 退出当前实例程序
                    Environment.Exit(0);
                }
            }
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                //var stackInfo = new StackTrace(true);
                App_SingleStart();
                #region 初始化IOC容器
                var serviceCollection = new ServiceCollection();
                //添加本地存储数据库
                serviceCollection.AddDbContext<LocalTestResultDbContext>();
                AddService(serviceCollection);
                var autofacServiceProviderFactory = new AutofacServiceProviderFactory();
                var builder = autofacServiceProviderFactory.CreateBuilder(serviceCollection);
                BaseRegister(builder);
                IocRegister(builder);
                var serviceProvider = autofacServiceProviderFactory.CreateServiceProvider(builder);
                IocHelper.Default.ConfigureServices(serviceProvider);
                #endregion
                #region 创建本地存储数据库
                var localDb = IocHelper.Default.GetService<LocalTestResultDbContext>();
                localDb?.Database.EnsureCreated();
                #endregion
                #region 初始化完成后调用
                OnInitComplate();
                #endregion
                #region 初始化UiLog
                LogHelper.RegisterUiLogEvent(new System.Action<object?, log4net.Core.LoggingEvent>((sender, e) =>
                {
                    if (UiLogViewModel.LogSource.Count <= 0 || UiLogViewModel.LogSource.Last().RenderedMessage != e.RenderedMessage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (UiLogViewModel.LogSource.Count > 999)
                            {
                                UiLogViewModel.LogSource.RemoveAt(0);
                            }
                            UiLogViewModel.LogSource.Add(e);
                        });
                    }
                }));
                #endregion
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "启动异常", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        protected abstract void AddService(ServiceCollection serviceCollection);
        protected abstract void IocRegister(ContainerBuilder builder);

        

        protected virtual void OnInitComplate()
        { }

        private void BaseRegister(ContainerBuilder builder)
        {
            //var dataAccess = Assembly.GetExecutingAssembly();
            var dataAccess = Assembly.GetEntryAssembly();
            if (dataAccess is { })
            {
                builder.RegisterAssemblyTypes(dataAccess)
                    .Where(t => t.Name.EndsWith("ViewModel") || t.Name.EndsWith("View"))
                    .AsSelf();
            }
            builder.RegisterType<UiLogViewModel>();
        }
    }
}
