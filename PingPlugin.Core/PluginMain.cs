using System.Windows.Forms;


namespace Qitana.PingPlugin
{
    public class PluginMain
    {
        public PluginMain()
        {
            UpdateChecker.Check();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            ((TabControl)pluginScreenSpace.Parent).TabPages.Remove(pluginScreenSpace);
        }

        public void DeInitPlugin()
        {
        }
    }
}
