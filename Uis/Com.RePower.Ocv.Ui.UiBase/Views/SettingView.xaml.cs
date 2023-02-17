using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Ui.UiBase.CustomControls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace Com.RePower.Ocv.Ui.UiBase.Views
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
        }


        public object SettingObj
        {
            get { return (object)GetValue(SettingObjProperty); }
            set { SetValue(SettingObjProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SettingObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingObjProperty =
            DependencyProperty.Register("SettingObj", typeof(object), typeof(SettingView), new PropertyMetadata(default(object),OnSettingObjChanged));

        private static void OnSettingObjChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (object)e.NewValue;
            var oldValue = (object)e.OldValue;
            if (oldValue == newValue)
                return;
            var target = d as SettingView;
            target?.OnSettingObjChanged(newValue, oldValue);
        }

        private void OnSettingObjChanged(object newValue, object oldValue)
        {
            this.SettingObj = newValue;
            var properties = SettingObj.GetType().GetProperties();
            List<SettingViewItem> itemList = new List<SettingViewItem>();
            foreach(var item in properties)
            {
                var attr = item.GetCustomAttribute<IgnorSettingAttribute>();
                if (attr == null)
                {
                    SettingViewItem temp = new SettingViewItem
                    {
                        DependObj = SettingObj,
                        PropertyInfo = item,
                    };
                    itemList.Add(temp); 
                }
            }
            this.OnPropertiesChanged(itemList);
        }


        public IEnumerable<SettingViewItem> Properties
        {
            get { return (IEnumerable<SettingViewItem>)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Properties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register("Properties", typeof(IEnumerable<SettingViewItem>), typeof(SettingView), new PropertyMetadata(new List<SettingViewItem>(), OnPropertiesChanged));

        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (IEnumerable<SettingViewItem>)e.NewValue;
            var oldValue = (IEnumerable<SettingViewItem>)e.OldValue;
            if (oldValue == newValue)
                return;
            var target = d as SettingView;
            target?.OnPropertiesChanged(newValue, oldValue);
        }

        private void OnPropertiesChanged(IEnumerable<SettingViewItem> newValue, IEnumerable<SettingViewItem>? oldValue = null)
        {
            this.Properties = newValue;
        }

        public Visibility SaveButtonVisibility
        {
            get
            {
                if(CanSaved && (SettingObj is ISettingSaveChanged))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public bool CanSaved { get; set; } = true;

        private async void DoSaveChanged(object sender, RoutedEventArgs e)
        {
            if(SettingObj is ISettingSaveChanged tempSetting)
            {
                var result = await tempSetting.SaveChangedAsync();
                if (result.IsFailed)
                    this.SettingViewSnackbar.MessageQueue?.Enqueue($"保存失败:{result.Message}");
                this.SettingViewSnackbar.MessageQueue?.Enqueue($"保存成功");
            }
            else
                this.SettingViewSnackbar.MessageQueue?.Enqueue($"当前对象非继承自\"ISettingSaveChanged\"接口");
        }
    }
    public partial class SettingViewItem:ObservableObject
    {
        public object? DependObj { get; set; }
        [NotifyPropertyChangedFor(nameof(SettingName))]
        [NotifyPropertyChangedFor(nameof(Value))]
        [NotifyPropertyChangedFor(nameof(Content))]
        [ObservableProperty]
        public PropertyInfo? _propertyInfo;
        public string? SettingName
        {
            get
            {
                var attr = PropertyInfo?.GetCustomAttribute(typeof(SettingNameAttribute));
                if (attr is SettingNameAttribute settingName)
                {
                    return settingName.Name;
                }
                return PropertyInfo?.Name;
            }
        }
        public object? Value
        {
            get { return PropertyInfo?.GetValue(DependObj); }
            set 
            {
                if (value?.GetType().IsAssignableFrom(PropertyInfo?.PropertyType) ?? false) 
                {
                    PropertyInfo?.SetValue(DependObj, value);
                }
                else
                {
                    var convertValue = Convert.ChangeType(value, PropertyInfo?.PropertyType ?? typeof(string));
                    if(convertValue?.GetType().IsAssignableFrom(PropertyInfo?.PropertyType) ?? false)
                    {
                        PropertyInfo?.SetValue(DependObj, convertValue);
                    }
                }
                OnPropertyChanged();
            }
        }
        public Control? Content
        {
            get { return CreateGenerateContent(); }
        }

        private Control? CreateGenerateContent()
        {
            Control? contentControl = null;
            var propertyType = PropertyInfo?.PropertyType;
            if(propertyType is { })
            {
                if (propertyType.IsEnum)
                {
                    contentControl = new ComboBox();
                    if (contentControl is ComboBox tempControl)
                    {
                        tempControl.ItemsSource = Enum.GetValues(typeof(Enum));
                        Binding valueBinding = new Binding();
                        valueBinding.Source = this;
                        valueBinding.Path = new PropertyPath("Value");
                        tempControl.SetBinding(ComboBox.SelectedItemProperty, valueBinding);
                    }
                }
                else if (typeof(string).IsAssignableFrom(propertyType)) 
                {
                    contentControl = new TextBox();
                    if (contentControl is TextBox tempControl)
                    {
                        Binding valueBinding = new Binding();
                        valueBinding.Source = this;
                        valueBinding.Path = new PropertyPath("Value");
                        tempControl.SetBinding(TextBox.TextProperty, valueBinding);
                    }
                }
                else if (typeof(bool).IsAssignableFrom(propertyType))
                {
                    contentControl = new CheckBox();
                    if (contentControl is CheckBox tempControl)
                    {
                        Binding valueBinding = new Binding();
                        valueBinding.Source = this;
                        valueBinding.Path = new PropertyPath("Value");
                        tempControl.SetBinding(CheckBox.IsCheckedProperty, valueBinding);
                    }
                }
                else
                {
                    contentControl = new TextBox();
                    if (contentControl is TextBox tempControl)
                    {
                        Binding valueBinding = new Binding();
                        valueBinding.Source = this;
                        valueBinding.Path = new PropertyPath("Value");
                        tempControl.SetBinding(TextBox.TextProperty, valueBinding);
                    }
                }
                contentControl.VerticalAlignment = VerticalAlignment.Center;
            }
            return contentControl;
        }
    }
}
