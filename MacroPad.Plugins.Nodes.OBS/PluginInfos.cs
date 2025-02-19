using MacroPad.Plugins.Nodes.Discord;
using MacroPad.Plugins.Nodes.OBS;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class PluginInfos : IPluginInfos
    {
        public string Name => "OBS";

        public string Description => "A plugin that allows you to interact with OBS using MacroPad.";

        public string Version => "1.0.0";

        public string Author => "Piripe";

        public string? AuthorUrl => null;

        public string? SourceUrl => null;

        public IProtocol[] Protocols => [ new OBSProtocol() ];

        public INodeCategory[] NodeCategories => [ new OBSCategory() ];

        public NodeType[] NodeTypes => [
            OBS.NodeTypes.obsSceneType
        ];
        public ISettingsComponent[] Settings => [];
    }
}