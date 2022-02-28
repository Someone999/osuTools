using System.Runtime.InteropServices;
using osuTools.MemoryCache.Win32.Structures;

namespace osuTools.MemoryCache.Win32.Api
{
    static class Win32Api
    {

        [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool GlobalMemoryStatusEx(out MemoryStatusEx status);
    }
}
