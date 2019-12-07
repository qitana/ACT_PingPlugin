using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Advanced_Combat_Tracker;
using System.Reflection;

namespace Qitana.PingPlugin
{
    public static class FFXIVProcessHelper
     {
        private static ActPluginData _plugin;
        public static Process GetFFXIVProcess
        {
            get
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

                    if(_plugin == null)
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
        }
     }
}

