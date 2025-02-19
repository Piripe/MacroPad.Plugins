using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

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

        public IProtocol[] Protocols => [];

        public INodeCategory[] NodeCategories => [];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}