using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OOC.Util
{
    class SysUtil
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORYSTATUSEX
        {
            internal uint dwLength;
            internal uint dwMemoryLoad;
            internal ulong ullTotalPhys;
            internal ulong ullAvailPhys;
            internal ulong ullTotalPageFile;
            internal ulong ullAvailPageFile;
            internal ulong ullTotalVirtual;
            internal ulong ullAvailVirtual;
            internal ulong ullAvailExtendedVirtual;
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        public static long getTotalRamSize()
        {
            MEMORYSTATUSEX statEX = new MEMORYSTATUSEX();
            GlobalMemoryStatusEx(statEX);
            return (long)statEX.ullTotalPhys;
        }

        public static long getAvailableRamSize()
        {
            MEMORYSTATUSEX statEX = new MEMORYSTATUSEX();
            GlobalMemoryStatusEx(statEX);
            return (long)statEX.ullAvailPhys;
        }

        public static int getProcessCount()
        {
            return Process.GetProcesses().Length;
        }

        public static double getLoadAverage()
        {
            // TODO
            return 0.0;
        }
    }
}
