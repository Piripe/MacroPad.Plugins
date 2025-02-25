using MacroPad.Plugins.Nodes.Discord;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class PluginInfos : IPluginInfos
    {
        public IProtocol[] Protocols => [];

        public INodeCategory[] NodeCategories => [new DiscordCategory()];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}