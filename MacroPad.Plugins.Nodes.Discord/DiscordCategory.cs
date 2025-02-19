using MacroPad.Shared.Media;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.Discord
{
    internal class DiscordCategory : INodeCategory
    {
        public string Name => "Discord";

        public string Id => "Discord";

        public Color Color => new(40, 40, 40);

        public INodeGetter[] Getters => [];

        public INodeRunner[] Runners => [];

        public DiscordCategory()
        {

        }
    }
}
