using MacroPad.Plugins.Nodes.Discord;
using MacroPad.Shared.Plugin;

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

        public IProtocol[] Protocols => new IProtocol[0];

        public INodeCategory[] NodeCategories => new INodeCategory[] { new DiscordCategory() };

        public NodeType[] NodeTypes => new NodeType[0];
    }
}