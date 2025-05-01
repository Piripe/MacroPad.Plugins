using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;

namespace MacroPad.Plugins.Nodes.VoiceMeeter.Nodes
{
    internal class GetTextParameter : INodeGetter
    {
        public string Name => "Get Text Parameter";

        public string Description => "Get a parameter that have a text value.";

        public string Id => "GetTextParameter";

        public TypeNamePair[] Inputs => [new(typeof(string), "")];

        public TypeNamePair[] Outputs => [new(typeof(string), "")];

        public INodeComponent[] Components => [];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;
        public object[] GetOutputs(INodeResourceManager resource)
        {
            string value = (string)resource.GetValue(0);
            if (value != null) return [VoiceMeeterRemote.GetString(value)];
            return [""];
        }
    }
}
