using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes.Components;
using MacroPad.Shared.Plugin.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class GetTextParameter : INodeGetter
    {
        public string Name => "Get Text Parameter";

        public string Description => "Get a parameter that have a text value.";

        public string Id => "GetTextParameter";

        public TypeNamePair[] Inputs => new TypeNamePair[] { new TypeNamePair(typeof(string), "") };

        public TypeNamePair[] Outputs => new TypeNamePair[] { new TypeNamePair(typeof(string), "") };

        public INodeComponent[] Components => new INodeComponent[] { };

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;
        public object[] GetOutputs(IResourceManager resource)
        {
            string value = (string)resource.GetValue(0);
            if (value != null) return new object[] { VoiceMeeterRemote.GetString(value) };
            return new object[] { "" };
        }
    }
}
