using MacroPad.Shared.Media;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.Discord
{
    internal class DiscordCategory : INodeCategory
    {
        public string Name => "Discord";

        public string Id => "Discord";

        public Color Color => new Color(40, 40, 40);

        public INodeGetter[] Getters => new INodeGetter[] { };

        public INodeRunner[] Runners => new INodeRunner[] { };

        public DiscordCategory()
        {

        }
    }
}
