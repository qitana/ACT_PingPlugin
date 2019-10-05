using System;
using System.Runtime.InteropServices;

namespace Qitana.PingPlugin
{
    static class NativeMethods
    {
        public enum TCP_TABLE_CLASS
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public int State;
            public int LocalAddr;
            public int LocalPort;
            public int RemoteAddr;
            public int RemotePort;
            public int OwningPid;
        }

        public static string[] StateStrings = {
            "",
            "CLOSED",
            "LISTENING",
            "SYN_SENT",
            "SYN_RCVD",
            "ESTABLISHED",
            "FIN_WAIT1",
            "FIN_WAIT2",
            "CLOSE_WAIT",
            "CLOSING",
            "LAST_ACK",
            "TIME_WAIT",
            "DELETE_TCB"
        };

        [DllImport("iphlpapi.dll")]
        extern public static int GetExtendedTcpTable(
            IntPtr pTcpTable, 
            ref int pdwSize, 
            bool bOrder, 
            uint ulAf, 
            TCP_TABLE_CLASS TableClass, 
            int Reserved);

    }
}
