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
    internal class SetTextParameter : INodeRunner
    {
        public string Name => "Set Text Parameter";

        public string Description => "Set a parameter that have a text value.";

        public string Id => "SetTextParameter";

        public TypeNamePair[] Inputs => new TypeNamePair[] { new TypeNamePair(typeof(string), ""), new TypeNamePair(typeof(string), "") };

        public TypeNamePair[] Outputs => new TypeNamePair[] { };

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => new string[0];

        public INodeComponent[] Components => new INodeComponent[] { };

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(IResourceManager resource)
        {
            VoiceMeeterRemote.SetParameter((string)resource.GetValue(0), (string)resource.GetValue(1));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = new object[0] };
        }
    }
}
