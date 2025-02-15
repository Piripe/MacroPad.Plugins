using MacroPad.Shared.Plugin;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class PluginInfos : IPluginInfos
    {
        public string Name => "VoiceMeeter";

        public string Description => "A plugin that allows you to interact with VoiceMeeter using MacroPad.";

        public string Version => "1.0.0";

        public string Author => "Piripe";

        public string? AuthorUrl => null;

        public string? SourceUrl => null;

        public IProtocol[] Protocols => new IProtocol[0];

        public INodeCategory[] NodeCategories => new INodeCategory[] { new VoiceMeeterCategory() };

        public NodeType[] NodeTypes => new NodeType[0];
    }
}