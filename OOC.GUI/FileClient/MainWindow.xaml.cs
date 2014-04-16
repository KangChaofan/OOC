﻿using System;
using System.Drawing;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using ActiproSoftware.Windows.Controls.Navigation;
using FileClient.FileService;
using FileClient.View;
using FileClient.WindowEffect;
using OOC.Util;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;

namespace FileClient
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileItemView fileItemView = new FileItemView();
        private readonly FileServiceClient Client = new FileServiceClient();
//        private readonly Logger _logger = new Logger("OOC.GUI.FileClient.log");

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public string CurrentPath { get; set; }

        public void Init()
        {
            //init here
            Height = 675;
            Width = 1200;

            menuDock.Visibility = Visibility.Collapsed;

            fileItemView = new FileItemView()
                {
                    Name = ""
                };
            Binding binding = new Binding();
            binding.Source = fileItemView;
            binding.Path = new PropertyPath("SubItems");
            treeView.SetBinding(TreeView.ItemsSourceProperty, binding);
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
            ApplyGlassEffect(addressBorder);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                menuDock.Visibility = Visibility.Visible;
            }
        }

        private void ApplyGlassEffect(Border border)
        {
            const int borderWidth = 1 + 2;
            const int dpi = 96;

            try
            {
                Application.Current.MainWindow.Background = Brushes.Transparent;
                //menu.Background = Brushes.Transparent;
                // Obtain the window handle for WPF application
                IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                if (mainWindowSrc != null)
                    if (mainWindowSrc.CompositionTarget != null)
                        mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Get System Dpi
                Graphics desktop = Graphics.FromHwnd(mainWindowPtr);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Set Margins
                var margins = new NonClientRegionAPI.MARGINS
                    {
                        cxLeftWidth = Convert.ToInt32(borderWidth*(DesktopDpiX/dpi)),
                        cxRightWidth = Convert.ToInt32(borderWidth*(DesktopDpiX/dpi)),
                        cyTopHeight = Convert.ToInt32(((int) border.ActualHeight + borderWidth + 25)*(DesktopDpiY/dpi)),
                        cyBottomHeight = Convert.ToInt32(borderWidth*(DesktopDpiY/dpi))
                    };

                // Extend glass frame into client area
                // Note that the default desktop Dpi is 96dpi. The  margins are
                // adjusted for the system Dpi.

                if (mainWindowSrc != null)
                {
                    int hr = NonClientRegionAPI.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                    //
                    if (hr < 0)
                    {
                        //DwmExtendFrameIntoClientArea Failed
                    }
                }
            }
                // If not Vista or up, paint background white.
            catch (DllNotFoundException)
            {
                Application.Current.MainWindow.Background = Brushes.White;
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
            catch (FaultException ex)
            {
//                _logger.Warn(ex.Message);
            }
            //throw new NotImplementedException();
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //CurrentPath = e.NewValue.CastTo<FileItemView>().Name;
        }
    }
}