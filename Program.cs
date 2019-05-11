using System;
using Linux;
using Windows;
using System.Runtime.InteropServices;

namespace Meminfo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WindowsMemInfo meminfo = new WindowsMemInfo();
                Console.WriteLine(meminfo.GetTotalPhysMemorySize());
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                LinuxMemInfo meminfo = new LinuxMemInfo();
                Console.WriteLine(meminfo.GetMemorySize());
            }
        }
    }

}