using MacroPad.Plugins.Nodes.Discord;
using MacroPad.Plugins.Nodes.OBS;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class PluginInfos : IPluginInfos
    {
        public IProtocol[] Protocols => [ new OBSProtocol() ];

        public INodeCategory[] NodeCategories => [ new OBSCategory() ];

        public NodeType[] NodeTypes => [
            OBS.NodeTypes.obsSceneType
        ];
        public ISettingsComponent[] Settings => [];
    }
}