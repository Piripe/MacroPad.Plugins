using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Nodes.Javascript
{
    public class PluginInfos : IPluginInfos
    {
        public IProtocol[] Protocols => [];

        public INodeCategory[] NodeCategories => [];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}