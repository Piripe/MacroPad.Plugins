using MacroPad.Plugins.Protocol.Midi.Protocol;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Settings;

namespace MacroPad.Plugins.Protocol.Midi
{
    public class PluginInfos : IPluginInfos
    {
        public string Name => "MIDI Protocol";

        public string Description => "A plugin that allows you to connect any MIDI device to MacroPad.";

        public string Version => "1.0.0";

        public string Author => "Piripe";

        public string? AuthorUrl => null;

        public string? SourceUrl => null;

        public IProtocol[] Protocols => [new MidiProtocol()];

        public INodeCategory[] NodeCategories => [];

        public NodeType[] NodeTypes => [];
        public ISettingsComponent[] Settings => [];
    }
}