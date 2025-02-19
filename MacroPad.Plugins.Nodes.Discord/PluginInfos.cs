using MacroPad.Plugins.Nodes.Discord;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class PluginInfos : IPluginInfos
    {
        public string Name => "Discord";

        public string Description => "A plugin that allows you to interact with Discord using MacroPad.";

        public string Version => "1.0.0";

        public string Author => "Piripe";

        public string? AuthorUrl => null;

        public string? SourceUrl => null;

        public IProtocol[] Protocols => [];

        public INodeCategory[] NodeCategories => [new DiscordCategory()];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}