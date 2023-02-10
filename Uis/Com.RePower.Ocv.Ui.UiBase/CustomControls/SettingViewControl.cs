using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace Com.RePower.Ocv.Ui.UiBase.CustomControls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Com.RePower.Ocv.Ui.UiBase.CustomControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Com.RePower.Ocv.Ui.UiBase.CustomControls;assembly=Com.RePower.Ocv.Ui.UiBase.CustomControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:SettingViewControl/>
    ///
    /// </summary>
    public class SettingViewControl : ContentControl
    {
        static SettingViewControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingViewControl), new FrameworkPropertyMetadata(typeof(SettingViewControl)));
        }

        public Object SettingObj
        {
            get { return (Object)GetValue(SettingObjProperty); }
            set { SetValue(SettingObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingObjProperty =
            DependencyProperty.Register("SettingObj", typeof(Object), typeof(SettingViewControl), new PropertyMetadata(default(object),OnSettingObjChanged));

        private static void OnSettingObjChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (object)e.OldValue;
            var newValue = (object)e.NewValue;
            if (oldValue == newValue)
                return;
            var target = d as SettingViewControl;
            target?.OnSettingObjChanged(newValue, oldValue);
        }

        public IEnumerable<SettingViewControlItem> PropertyList
        {
            get { return (IEnumerable<SettingViewControlItem>)GetValue(PropertyListProperty); }
            set { SetValue(PropertyListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PropertyList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyListProperty =
            DependencyProperty.Register("PropertyList", typeof(IEnumerable<SettingViewControlItem>), typeof(SettingViewControl), new PropertyMetadata(new List<SettingViewControlItem>(),OnPropertyListChanged));

        private static void OnPropertyListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (IEnumerable<SettingViewControlItem>)e.OldValue;
            var newValue = (IEnumerable<SettingViewControlItem>)e.NewValue;
            if (oldValue == newValue)
                return;
            var target = d as SettingViewControl;
            target?.OnPropertyListChanged(newValue, oldValue);
        }

        protected virtual void OnSettingObjChanged(object newValue,object oldValue)
        {
            this.SettingObj = newValue;
            var properties = SettingObj.GetType().GetProperties();
            List<SettingViewControlItem> settingViewControlItems= new List<SettingViewControlItem>();
            foreach ( var property in properties )
            {
                SettingViewControlItem temp = new SettingViewControlItem()
                {
                    DependObj = SettingObj,
                    PropertyInfo = property,
                };
            }
            OnPropertyListChanged(settingViewControlItems);
        }
        protected virtual void OnPropertyListChanged(IEnumerable<SettingViewControlItem> newValue,IEnumerable<SettingViewControlItem>? oldValue = null)
        {
            this.PropertyList = newValue;
        }
    }
    public class SettingViewControlItem
    {
        public object? DependObj { get; set; }
        public PropertyInfo? PropertyInfo { get; set; }
        public string? SettingName
        {
            get { return PropertyInfo?.Name; }
        }
        public object? Value
        {
            get { return PropertyInfo?.GetValue(DependObj); }
            set { PropertyInfo?.SetValue(DependObj, value); }
        }
    }
}
