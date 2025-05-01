using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class SetTextParameter : INodeRunner
    {
        public string Name => "Set Text Parameter";

        public string Description => "Set a parameter that have a text value.";

        public string Id => "SetTextParameter";

        public TypeNamePair[] Inputs => [new(typeof(string), ""), new(typeof(string), "")];

        public TypeNamePair[] Outputs => [];

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => [];

        public INodeComponent[] Components => [];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(INodeResourceManager resource)
        {
            VoiceMeeterRemote.SetParameter((string)resource.GetValue(0), (string)resource.GetValue(1));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = [] };
        }
    }
}
