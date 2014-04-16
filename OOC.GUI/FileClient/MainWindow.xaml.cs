using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Navigation;
using FileClient.Annotations;
using FileClient.FileService;
using FileClient.View;
using FileClient.WindowEffect;
using OOC.Util;

namespace FileClient
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly FileServiceClient Client = new FileServiceClient();
        private FileItemView _currentPath;
        private FileItemView rootPath = new FileItemView();
        //        private readonly Logger _logger = new Logger("OOC.GUI.FileClient.log");

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public FileItemView CurrentPath
        {
            get { return _currentPath; }
            set
            {
                _currentPath = value;
                OnPropertyChanged("CurrentPath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Init()
        {
            //init here
            Height = 675;
            Width = 1200;

            menuDock.Visibility = Visibility.Collapsed;


            rootPath = new FileItemView
            {
                SubItems = new ObservableCollection<FileItemView>
                        {
                            new FileItemView
                                {
                                    Name = "",
                                    DisplayName = "Root",
                                    Icon = new BitmapImage(new Uri(@"Resources/Images/Computer16.png",
                                                                   UriKind.RelativeOrAbsolute)),
                                }
                        }
            };
            CurrentPath = rootPath;

            SetBindings();
        }

        private void SetBindings()
        {
            TreeView.SetBinding(ItemsControl.ItemsSourceProperty,
                                new Binding {Source = rootPath, Path = new PropertyPath("SubItems"),Mode = BindingMode.OneTime});
            ListView.SetBinding(ItemsControl.ItemsSourceProperty,
                                new Binding
                                    {
                                        Source = CurrentPath,
                                        Path = new PropertyPath("SubItems"),
                                    });
//            StatusText.SetBinding(TextBlock.TextProperty, new Binding
//                {
//                    Source = _currentPath,
//                    Path = new PropertyPath("DisplayName")
//                });
        }

        public void NavigateTo(string path)
        {
            try
            {
                FileSystemDescription[] fileDescriptions = Client.List(path);
            }
            catch (FaultException e)
            {
                //                _logger.Warn(e.Message);
            }
        }

        private void menu_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            menuDock.Visibility = Visibility.Visible;
        }

        private void menu_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            menuDock.Visibility = Visibility.Collapsed;
        }

        private void menu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            menuDock.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlassEffect.ApplyGlassEffect(this, addressBorder);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                menuDock.Visibility = Visibility.Visible;
            }
        }

        private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e)
        {
            string path = e.Path;
            var item = e.Item as BreadcrumbItem;
            if (item == null) return;
            ItemCollection items = item.Items;
            items.Clear();
            try
            {
                FileSystemDescription filedesc = Client.Stat(path);
                if (filedesc.IsDirectory)
                {
                    //                    _logger.Debug(string.Format("[LIST]{0}", path));
                    FileSystemDescription[] fileDescriptions = Client.List(path);
                    foreach (FileSystemDescription file in fileDescriptions)
                    {
                        items.Add(new BreadcrumbItem
                            {
                                Header = file.Name,
                                PathEntry = file.Name,
                            });
                    }
                }
            }
            catch (FaultException)
            {
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CurrentPath = e.NewValue.CastTo<FileItemView>();
            StatusText.Text = string.Format("已选中 {0}, 大小 {1}字节, 上次访问 {2}", _currentPath.DisplayName, _currentPath.Size,
                                            _currentPath.AccessTime);
            ListView.SetBinding(ItemsControl.ItemsSourceProperty,
                    new Binding
                    {
                        Source = CurrentPath,
                        Path = new PropertyPath("SubItems"),
                    });
//            SetBindings();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}