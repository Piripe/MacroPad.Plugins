using MacroPad.Plugins.Nodes.OBS.Nodes;
using MacroPad.Shared.Media;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.Discord
{
    internal class OBSCategory : INodeCategory
    {
        public string Name => "OBS";

        public string Id => "OBS";

        public Color Color => new(40, 40, 40);

        public INodeGetter[] Getters => [];

        public INodeRunner[] Runners => [new SetCurrentScene()];

        public OBSCategory()
        {

        }
    }
}
