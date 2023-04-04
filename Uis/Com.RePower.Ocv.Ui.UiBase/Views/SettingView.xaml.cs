using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Com.RePower.Ocv.Ui.UiBase.Views
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        public object SettingObj
        {
            get => GetValue(SettingObjProperty);
            set => SetValue(SettingObjProperty, value);
        }

        // Using a DependencyProperty as the backing store for SettingObj.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingObjProperty =
            DependencyProperty.Register(nameof(SettingObj), typeof(object), typeof(SettingView), new PropertyMetadata(default(object), OnSettingObjChanged));

        private static void OnSettingObjChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newValue = e.NewValue;
            var oldValue = e.OldValue;
            if (oldValue == newValue)
                return;
            var target = d as SettingView;
            target?.OnSettingObjChanged(newValue);
        }

        private void OnSettingObjChanged(object newValue)
        {
            this.SettingObj = newValue;
            var properties = SettingObj.GetType().GetProperties();
            var itemList = (from item in properties let attr = item.GetCustomAttribute<IgnorSettingAttribute>() where attr == null select new SettingViewItem { DependObj = SettingObj, PropertyInfo = item, }).ToList();
            this.SaveButton.Visibility = SaveButtonVisibility;
            this.OnPropertiesChanged(itemList);
        }

        public IEnumerable<SettingViewItem> Properties
        {
            get => (IEnumerable<SettingViewItem>)GetValue(PropertiesProperty);
            set => SetValue(PropertiesProperty, value);
        }

        // Using a DependencyProperty as the backing store for Properties.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register(nameof(Properties), typeof(IEnumerable<SettingViewItem>), typeof(SettingView), new PropertyMetadata(new List<SettingViewItem>(), OnPropertiesChanged));

        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (IEnumerable<SettingViewItem>)e.NewValue;
            var oldValue = (IEnumerable<SettingViewItem>)e.OldValue;
            if (Equals(oldValue, newValue))
                return;
            var target = d as SettingView;
            target?.OnPropertiesChanged(newValue);
        }

        private void OnPropertiesChanged(IEnumerable<SettingViewItem> newValue)
        {
            this.Properties = newValue;
        }

        public Visibility SaveButtonVisibility
        {
            get
            {
                if (CanSaved && (SettingObj is ISettingSaveChanged))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public bool CanSaved { get; set; } = true;

        private async void DoSaveChanged(object sender, RoutedEventArgs e)
        {
            if (SettingViewSnackbar.MessageQueue == null)
            {
                SettingViewSnackbar.MessageQueue = new MaterialDesignThemes.Wpf.SnackbarMessageQueue();
            }
            if (SettingObj is ISettingSaveChanged tempSetting)
            {
                var result = await tempSetting.SaveChangedAsync();
                if (result.IsFailed)
                    this.SettingViewSnackbar.MessageQueue?.Enqueue($"保存失败:{result.Message}");
                else
                    this.SettingViewSnackbar.MessageQueue?.Enqueue($"保存成功");
            }
            else
                this.SettingViewSnackbar.MessageQueue?.Enqueue($"当前对象非继承自\"ISettingSaveChanged\"接口");
        }
    }

    public class SettingViewItem : ObservableObject
    {
        public object? DependObj { get; set; }

        private PropertyInfo? _propertyInfo;

        public PropertyInfo? PropertyInfo
        {
            get => _propertyInfo;
            set
            {
                if (!SetProperty(ref _propertyInfo, value)) return;
                OnPropertyChanged(nameof(SettingName));
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Content));
            }
        }

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
            get => PropertyInfo?.GetValue(DependObj);
            set
            {
                if (value?.GetType().IsAssignableFrom(PropertyInfo?.PropertyType) ?? false)
                {
                    PropertyInfo?.SetValue(DependObj, value);
                }
                else
                {
                    if ((PropertyInfo?.PropertyType.IsGenericType ?? true) && PropertyInfo?.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (value == null)
                        {
                            PropertyInfo?.SetValue(DependObj, null);
                        }
                        else
                        {
                            var convertValue = Convert.ChangeType(value, PropertyInfo?.PropertyType.GetGenericArguments()[0] ?? typeof(string));
                            PropertyInfo?.SetValue(DependObj, convertValue);
                        }
                    }
                    else
                    {
                        var convertValue = Convert.ChangeType(value, PropertyInfo?.PropertyType ?? typeof(string));
                        PropertyInfo?.SetValue(DependObj, convertValue);
                    }
                }
                OnPropertyChanged();
            }
        }

        public Control? Content => CreateGenerateContent();

        private Control? CreateGenerateContent()
        {
            Control? contentControl = null;
            var propertyType = PropertyInfo?.PropertyType;
            if (propertyType is null) return contentControl;
            if (propertyType.IsEnum)
            {
                contentControl = new ComboBox();
                if (contentControl is ComboBox tempControl)
                {
                    tempControl.ItemsSource = Enum.GetValues(typeof(Enum));
                    var valueBinding = new Binding();
                    valueBinding.Source = this;
                    valueBinding.Path = new PropertyPath("Value");
                    tempControl.SetBinding(Selector.SelectedItemProperty, valueBinding);
                }
            }
            else if (typeof(string).IsAssignableFrom(propertyType))
            {
                contentControl = new TextBox();
                if (contentControl is TextBox tempControl)
                {
                    var valueBinding = new Binding
                    {
                        Source = this,
                        Path = new PropertyPath("Value")
                    };
                    tempControl.SetBinding(TextBox.TextProperty, valueBinding);
                }
            }
            else if (typeof(bool).IsAssignableFrom(propertyType))
            {
                contentControl = new CheckBox();
                if (contentControl is CheckBox tempControl)
                {
                    var valueBinding = new Binding
                    {
                        Source = this,
                        Path = new PropertyPath("Value")
                    };
                    tempControl.SetBinding(ToggleButton.IsCheckedProperty, valueBinding);
                }
            }
            else
            {
                contentControl = new TextBox();
                if (contentControl is TextBox tempControl)
                {
                    var valueBinding = new Binding { Source = this, Path = new PropertyPath("Value") };
                    tempControl.SetBinding(TextBox.TextProperty, valueBinding);
                }
            }
            contentControl.VerticalAlignment = VerticalAlignment.Center;
            return contentControl;
        }
    }
}