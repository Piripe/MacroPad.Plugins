using MacroPad.Plugins.Nodes.VoiceMeeter.Nodes;
using MacroPad.Shared.Media;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.VoiceMeeter
{
    public class VoiceMeeterCategory : INodeCategory
    {
        public string Name => "VoiceMeeter";

        public string Id => "VoiceMeeter";

        public Color Color => new Color(40,40,40);

        public INodeGetter[] Getters => new INodeGetter[] { new GetNumberParameter(), new GetTextParameter() };

        public INodeRunner[] Runners => new INodeRunner[] { new SetStripGain(), new SetBusGain(), new SetNumberParameter(), new SetTextParameter() };

        public VoiceMeeterCategory()
        {
            Static.Init();
        }
    }
}
