using MacroPad.Plugins.Protocol.Midi.Protocol;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Protocol.Midi
{
    public class PluginInfos : IPluginInfos
    {
        public IProtocol[] Protocols => [new MidiProtocol()];

        public INodeCategory[] NodeCategories => [];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}