using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using InfoReader.Tools.I8n;
using Microsoft.Win32.SafeHandles;
using osuTools.Exceptions;

namespace osuTools.GameInfo
{
    /// <summary>
    /// 进程检测器，可以等待进程启动和退出
    /// </summary>
    public class ProcessMonitor
    {

        [DllImport("psapi.dll",SetLastError = true)]
        static extern bool EnumProcesses(int[] pProcessIds, int cbSize,out int cbBytesReturned);

        [DllImport("psapi.dll",EntryPoint = "GetModuleFileNameExW", SetLastError = true)]
        static extern int GetModuleFileName(IntPtr hProcess,IntPtr hModule, byte[] lpFileName, int nSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetExitCodeProcess(IntPtr hProcess,out int exitCode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WaitForSingleObject(IntPtr handle, int millisec);

        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr OpenProcess(ProcessAccess access, bool inherit, int pid);
        /// <summary>
        /// 为OnExitCodeNonZero提供事件处理器
        /// </summary>
        /// <param name="exitCode">退出时的返回值</param>
        public delegate void NonZeroExitCode(int exitCode);
        /// <summary>
        /// 为OnExited提供事件处理器
        /// </summary>
        /// <param name="exitCode"></param>
        public delegate void Exited(int? exitCode);
        /// <summary>
        /// 进程退出时触发的事件，如果需要，Process的需要单独设置
        /// </summary>

        public event Exited OnExited;
        /// <summary>
        /// 退出返回值为非0时触发的事件
        /// </summary>
        public event NonZeroExitCode OnExitCodeNonZero;
        /// <summary>
        /// 当前的游戏进程句柄
        /// </summary>
        public SafeProcessHandle NativeProcessHandle;
        /// <summary>
        /// 当前游戏进程对象
        /// </summary>
        public Process CurrentProcess;
        /// <summary>
        /// 要等待的进程名
        /// </summary>
        public string TargetProcessName { get; set; }
        /// <summary>
        /// 使用指定的进程名初始化ProcessDetector
        /// </summary>
        /// <param name="targetProcessName">进程名</param>
        public ProcessMonitor(string targetProcessName)
        {
            if (targetProcessName.EndsWith(".exe"))
            {
                int len = targetProcessName.Length - 4;
                targetProcessName = targetProcessName.Substring(0, len);
            }
            TargetProcessName = targetProcessName;
        }
        /// <summary>
        /// 等待进程运行
        /// </summary>
        /// <param name="millisec">等待的毫秒数</param>
        /// <param name="interval">每次检测之间间隔的毫秒数</param>
        public void WaitForStart(int millisec, int interval)
        {
            Stopwatch s = new Stopwatch();
            if (millisec == 0)
                return;
            if(millisec > 1)
                s.Start();
            while (s.IsRunning && s.ElapsedMilliseconds < millisec || millisec < 0)
            {
                int size = 4 * 1024;
                int[] pids = new int[1024];
                EnumProcesses(pids, size, out _);
                foreach (var pid in pids)
                {
                    var handle = OpenProcess(ProcessAccess.QueryInformation | ProcessAccess.Synchronize, false, pid);
                    if (handle == IntPtr.Zero)
                        continue;
                    byte[] bts = new byte[512];
                    int pathLen = GetModuleFileName(handle, IntPtr.Zero, bts, 512);
                    if (pathLen == 0) 
                        continue;
                    var fullPath = Encoding.Unicode.GetString(bts,0,pathLen * sizeof(char));
                    var moduleName = Path.GetFileNameWithoutExtension(fullPath);
                    if (TargetProcessName != moduleName)
                        continue;
                    NativeProcessHandle = new SafeProcessHandle(handle, true);
                    CurrentProcess = Process.GetProcessById(pid);
                    s.Stop();
                    return;
                }
                Thread.Sleep(interval);
            }
        }
        void ThrowExceptionByCode(int code)
        {
            switch (code)
            {
                case 0x00000080: throw new ObjectWaitException(LocalizationInfo.Current.Translations["LANG_ERR_WAITABANDONED"]);
                case 0: throw new ObjectWaitException(LocalizationInfo.Current.Translations["LANG_ERR_WAITOBJECTSIGNALED"]);
                case 0x102: throw new Exception("LANG_ERR_WAITTIMEOUT");
                case -1: throw new ObjectWaitException(LocalizationInfo.Current.Translations["LANG_ERR_WAITFAILED"],
                    new Win32Exception(Marshal.GetLastWin32Error()));
                default: throw new Exception(LocalizationInfo.Current.Translations["LANG_ERR_WAITFAILED"]);
            }
        }
        /// <summary>
        /// 等待进程退出
        /// </summary>
        /// <param name="millisec">要等待的毫秒数，-1为无限等待</param>
        /// <param name="nativeWait">是否直接使用WaitForSingleObject等待</param>
        public int? WaitForExit(int millisec, bool nativeWait)
        {
            if (nativeWait)
            {
                
                int rslt = WaitForSingleObject(NativeProcessHandle.DangerousGetHandle(), millisec);
                ThrowExceptionByCode(rslt);
                if (GetExitCodeProcess(NativeProcessHandle.DangerousGetHandle(), out var exitCode))
                {
                    if (exitCode == 0)
                    {
                        OnExited?.Invoke(exitCode);
                    }
                    else
                    {
                        OnExitCodeNonZero?.Invoke(exitCode);
                    }
                    NativeProcessHandle = null;
                    return exitCode;
                }
                OnExited?.Invoke(null);
                NativeProcessHandle = null;
                return null;
            }
            CurrentProcess.WaitForExit(millisec);
            NativeProcessHandle = null;
            return CurrentProcess.ExitCode;
        }
    }
}
