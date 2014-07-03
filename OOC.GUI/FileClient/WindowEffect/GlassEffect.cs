using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;

namespace FileClient.WindowEffect
{
    public static class GlassEffect
    {
        public static void ApplyGlassEffect(Window window, Border border)
        {
            const int borderWidth = 1 + 2;
            const int dpi = 96;

            try
            {
                Application.Current.MainWindow.Background = Brushes.Transparent;
                //menu.Background = Brushes.Transparent;
                // Obtain the window handle for WPF application
                IntPtr mainWindowPtr = new WindowInteropHelper(window).Handle;
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
    }
}