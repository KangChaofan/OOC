<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OOC.Util
{
    public class SysUtil
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

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

        public static decimal getLoadAverage()
        {
            // TODO
            return 0.0M;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace OOC.Util
{
    public class SysUtil
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

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

        public static decimal getLoadAverage()
        {
            // TODO
            return 0.0M;
        }
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
