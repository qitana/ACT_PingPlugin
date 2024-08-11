using Advanced_Combat_Tracker;
using RainbowMage.OverlayPlugin;
using System.Windows.Forms;

namespace Qitana.PingPlugin
{
    internal class PluginLoader : IActPluginV1, IOverlayAddonV2
    {
        PluginMain _pluginMain;
        TabPage _pluginScreenSpace;
        Label _pluginStatusText;

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            _pluginScreenSpace = pluginScreenSpace;
            _pluginStatusText = pluginStatusText;

            _pluginStatusText.Text = "Initializing...";
            _pluginScreenSpace.Text = "PingPlugin";

            _pluginMain = new PluginMain();
            _pluginMain.InitPlugin(pluginScreenSpace, pluginStatusText);

            _pluginStatusText.Text = "Ready.";
        }

        public void DeInitPlugin()
        {
            if (_pluginMain != null)
            {
                _pluginMain.DeInitPlugin();
            }
        }

        public void Init()
        {
            var container = Registry.GetContainer();
            var registry = container.Resolve<Registry>();
            registry.StartEventSource(new PingEventSource(container));
        }


    }
}
