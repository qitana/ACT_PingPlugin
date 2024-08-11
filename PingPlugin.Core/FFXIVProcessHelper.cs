using Advanced_Combat_Tracker;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Qitana.PingPlugin
{
    public static class FFXIVProcessHelper
    {
        private static ActPluginData _plugin;

        public static List<string> GetFFXIVRemoteAddresses()
        {
            Process process = GetFFXIVProcess();
            if (process == null)
            {
                return new List<string>();
            }

            return GetRemoteAddressesFromProcess(process);
        }
        private static Process GetFFXIVProcess()
        {
            try
            {
                if (_plugin == null && ActGlobals.oFormActMain.Visible)
                {
                    foreach (ActPluginData plugin in ActGlobals.oFormActMain.ActPlugins)
                    {
                        if (plugin.pluginFile.Name == "FFXIV_ACT_Plugin.dll")
                        {
                            _plugin = plugin;
                            break;
                        }
                    }
                }

                if (_plugin == null)
                {
                    return null;
                }

                FFXIV_ACT_Plugin.FFXIV_ACT_Plugin ffxivPlugin = (FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)_plugin.pluginObj;
                return ffxivPlugin.DataRepository.GetCurrentFFXIVProcess() ?? Process.GetProcessesByName("ffxiv_dx11").OrderBy(x => x.Id).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        private static List<string> GetRemoteAddressesFromProcess(Process process)
        {
            List<string> result = new List<string>();

            int size = 0;
            uint AF_INET = 2; // IPv4

            // check size            
            NativeMethods.GetExtendedTcpTable(IntPtr.Zero, ref size, true, AF_INET, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            // alloc memory
            var p = Marshal.AllocHGlobal(size);

            // get tcp table           
            if (NativeMethods.GetExtendedTcpTable(p, ref size, true, AF_INET, NativeMethods.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0) == 0)
            {
                var num = Marshal.ReadInt32(p); // data counts, MIB_TCPTABLE_OWNER_PID.dwNumEntries
                var ptr = IntPtr.Add(p, 4);
                for (int i = 0; i < num; i++)
                {
                    var o = (NativeMethods.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(ptr, typeof(NativeMethods.MIB_TCPROW_OWNER_PID));
                    if (o.RemoteAddr == 0)
                    {
                        o.RemotePort = 0;
                    }

                    if (!process.HasExited && process.Id > 0 && process.Id == o.OwningPid)
                    {
                        if (NativeMethods.StateStrings[o.State] == "ESTABLISHED")
                        {
                            var b = BitConverter.GetBytes(o.RemoteAddr);
                            var remoteAddr = string.Format("{0}.{1}.{2}.{3}", b[0], b[1], b[2], b[3]);

                            if (remoteAddr == "0.0.0.0" || remoteAddr == "127.0.0.1")
                            {
                                continue;
                            }

                            result.Add(remoteAddr);
                        }
                    }

                    ptr = IntPtr.Add(ptr, Marshal.SizeOf(typeof(NativeMethods.MIB_TCPROW_OWNER_PID))); // next data
                }
                Marshal.FreeHGlobal(p);  // release memory
            }

            // return distinct
            return result.Distinct().ToList();
        }

    }
}
