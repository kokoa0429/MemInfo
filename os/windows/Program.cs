using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;   
using System.Runtime.InteropServices;

namespace Windows
{
    class WindowsMemInfo
    {
        public struct MemoryStatus
        {
            /*
                DWORD     dwLength;
                DWORD     dwMemoryLoad;
                DWORDLONG ullTotalPhys;
                DWORDLONG ullAvailPhys;
                DWORDLONG ullTotalPageFile;
                DWORDLONG ullAvailPageFile;
                DWORDLONG ullTotalVirtual;
                DWORDLONG ullAvailVirtual;
                DWORDLONG ullAvailExtendedVirtual;
             */
            public int  dwLength;
            public uint  dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        private static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            internal static extern void GlobalMemoryStatusEx(ref MemoryStatus lpbuffer);   
        }

        public ulong GetTotalPhysMemorySize()
        {
            try
            {
                MemoryStatus ms = new MemoryStatus();
                ms.dwLength = Marshal.SizeOf(ms);
                NativeMethods.GlobalMemoryStatusEx(ref ms);
                return ms.ullTotalPhys;
            }
            catch (Exception)
            {
                // throw (e);
                return 0;
            }
        }
    }
    
}
