using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Protocol;

namespace MacroPad.Plugins.Nodes.Javascript
{
    public class PluginInfos : IPluginInfos
    {
        public string Name => "JavaScript Plugin";

        public string Description => "A plugin that allows you to run JavaScript on MacroPad.";

        public string Version => "1.0.0";

        public string Author => "Piripe";

        public string? AuthorUrl => null;

        public string? SourceUrl => null;

        public IProtocol[] Protocols => new IProtocol[0];

        public INodeCategory[] NodeCategories => new INodeCategory[0];

        public NodeType[] NodeTypes => new NodeType[0];
    }
}