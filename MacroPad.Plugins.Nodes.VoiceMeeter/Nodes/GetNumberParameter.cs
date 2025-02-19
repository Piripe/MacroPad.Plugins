using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class GetNumberParameter : INodeGetter
    {
        public string Name => "Get Number Parameter";

        public string Description => "Get a parameter that have a number value (and boolean too).";

        public string Id => "GetNumberParameter";

        public TypeNamePair[] Inputs => [new(typeof(string),"")];

        public TypeNamePair[] Outputs => [new(typeof(decimal), "")];

        public INodeComponent[] Components => [];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;
        public object[] GetOutputs(IResourceManager resource)
        {
            string value = (string)resource.GetValue(0);
            if (value != null)
            {
                return [(decimal)VoiceMeeterRemote.GetFloat(value)];
            }
            return [0];
        }
    }
}
