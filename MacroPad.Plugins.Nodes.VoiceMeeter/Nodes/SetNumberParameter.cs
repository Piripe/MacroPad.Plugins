using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Components;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class SetNumberParameter : INodeRunner
    {
        public string Name => "Set Number Parameter";

        public string Description => "Set a parameter that have a number value (and boolean too).";

        public string Id => "SetNumberParameter";

        public TypeNamePair[] Inputs => [new(typeof(string),""), new(typeof(decimal), "")];

        public TypeNamePair[] Outputs => [];

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => [];

        public INodeComponent[] Components => [];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(INodeResourceManager resource)
        {
            string value = (string)resource.GetValue(0);
            if (value != null) _ = VoiceMeeterRemote.SetParameter(value, decimal.ToSingle((decimal)resource.GetValue(1)));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = [] };
        }
    }
}
