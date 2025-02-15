using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;
using MacroPad.Shared.Plugin.Nodes.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class SetBusGain : INodeRunner
    {
        public string Name => "Set Bus Gain";

        public string Description => "Set the gain of a bus.";

        public string Id => "SetBusGain";

        public TypeNamePair[] Inputs => new TypeNamePair[] { new TypeNamePair(typeof(decimal),"") };

        public TypeNamePair[] Outputs => new TypeNamePair[] { };

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => new string[0];

        public INodeComponent[] Components => new INodeComponent[] { 
            new ComboBox()
            {
               GetItems = (IResourceManager resource, IDeviceLayoutButton button, IDeviceOutput output) => {
                   Static.Update();
                   return Static.BusesName;
               },
               GetSelection = (IResourceManager resource) => resource.GetData<int>("v"),
               SelectionChanged = (IResourceManager resource, int index) => resource.SetData("v",index)
            }
        };

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(IResourceManager resource)
        {
            VoiceMeeterRemote.SetParameter($"Bus({resource.GetData<int>("v")}).Gain", decimal.ToSingle((decimal)resource.GetValue(0)));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = new object[0] };
        }
    }
}
