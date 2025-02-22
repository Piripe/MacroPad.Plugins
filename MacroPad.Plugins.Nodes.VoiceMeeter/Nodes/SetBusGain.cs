using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;
using MacroPad.Shared.Plugin.Components;
using MacroPad.Shared.Plugin;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class SetBusGain : INodeRunner
    {
        public string Name => "Set Bus Gain";

        public string Description => "Set the gain of a bus.";

        public string Id => "SetBusGain";

        public TypeNamePair[] Inputs => [new(typeof(decimal),"")];

        public TypeNamePair[] Outputs => [];

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => [];

        public INodeComponent[] Components => [ 
            new ComboBox()
            {
               GetItems = (IResourceManager resource) => {
                   Static.Update();
                   return Static.BusesName;
               },
               GetSelection = (IResourceManager resource) => resource.GetData<int>("v"),
               SelectionChanged = (IResourceManager resource, int index) => resource.SetData("v",index)
            }
        ];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(INodeResourceManager resource)
        {
            VoiceMeeterRemote.SetParameter($"Bus({resource.GetData<int>("v")}).Gain", decimal.ToSingle((decimal)resource.GetValue(0)));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = [] };
        }
    }
}
