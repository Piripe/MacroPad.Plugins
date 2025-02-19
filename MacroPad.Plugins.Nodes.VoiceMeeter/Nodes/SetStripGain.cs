using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;
using MacroPad.Shared.Plugin.Components;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class SetStripGain : INodeRunner
    {
        public string Name => "Set Strip Gain";

        public string Description => "Set the gain of a strip.";

        public string Id => "SetStripGain";

        public TypeNamePair[] Inputs => [new(typeof(decimal), "")];

        public TypeNamePair[] Outputs => [];

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => [];

        public INodeComponent[] Components => [
            new ComboBox()
            {
               GetItems = (IResourceManager resource, IDeviceLayoutButton button, IDeviceOutput output) => {
                   Static.Update();
                   return Static.StripsName;
               },
               GetSelection = (IResourceManager resource) => resource.GetData<int>("v"),
               SelectionChanged = (IResourceManager resource, int index) => resource.SetData("v",index)
            }
        ];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(IResourceManager resource)
        {
            VoiceMeeterRemote.SetParameter($"Strip({resource.GetData<int>("v")}).Gain", decimal.ToSingle((decimal)resource.GetValue(0)));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = [] };
        }
    }
}
