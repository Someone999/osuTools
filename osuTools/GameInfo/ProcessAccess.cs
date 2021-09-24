using System;

namespace osuTools.GameInfo
{

    /// <summary>
    /// 如有疑问请参阅 https://docs.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
    /// </summary>
    [Flags]
    enum ProcessAccess : int
    {
        Delete = 0x00010000,
        ReadControl = 0x00020000,
        Synchronize = 0x00100000,
        WriteDac = 0x00040000,
        WriteOwner = 0x00080000,
        CreateProcess = 0x0080,
        CreateThread = 0x0002,
        DuplicateHandle = 0x0040,
        QueryInformation = 0x0400,
        QueryLimitedInformation = 0x1000,
        SetInformation = 0x0200,
        SetQuota = 0x0100,
        SuspendResume = 0x0800,
        Terminate = 0x0001,
        VirtualMemoryOperation = 0x0008,
        VirtualMemoryRead = 0x0010,
        VirtualMemoryWrite = 0x0020
    }
}