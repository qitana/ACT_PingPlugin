using Advanced_Combat_Tracker;
using RainbowMage.OverlayPlugin;
using System.Windows.Forms;

namespace Qitana.PingPlugin
{
    public class PingPlugin : IActPluginV1, IOverlayAddonV2
    {
        public static string pluginPath = "";

        public PingPlugin()
        {
            UpdateChecker.Check();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            pluginStatusText.Text = "Ready.";

            // We don't need a tab here.
            ((TabControl)pluginScreenSpace.Parent).TabPages.Remove(pluginScreenSpace);

            foreach (var plugin in ActGlobals.oFormActMain.ActPlugins)
            {
                if (plugin.pluginObj == this)
                {
                    pluginPath = plugin.pluginFile.FullName;
                    break;
                }
            }
        }

        public void DeInitPlugin()
        {

        }

        public void Init()
        {
            Registry.RegisterEventSource<PingEventSource>();
        }

    }
}