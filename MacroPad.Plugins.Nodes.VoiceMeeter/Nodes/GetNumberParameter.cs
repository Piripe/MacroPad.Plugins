using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes.Components;
using MacroPad.Shared.Plugin.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class GetNumberParameter : INodeGetter
    {
        public string Name => "Get Number Parameter";

        public string Description => "Get a parameter that have a number value (and boolean too).";

        public string Id => "GetNumberParameter";

        public TypeNamePair[] Inputs => new TypeNamePair[] { new TypeNamePair(typeof(string),"") };

        public TypeNamePair[] Outputs => new TypeNamePair[] { new TypeNamePair(typeof(decimal), "") };

        public INodeComponent[] Components => new INodeComponent[] { };

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;
        public object[] GetOutputs(IResourceManager resource)
        {
            string value = (string)resource.GetValue(0);
            if (value != null)
            {
                return new object[] { (decimal)VoiceMeeterRemote.GetFloat(value) };
            }
            return new object[] { 0 };
        }
    }
}
