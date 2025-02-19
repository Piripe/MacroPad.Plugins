using MacroPad.Shared.Device;
using MacroPad.Shared.Plugin.Nodes;
using OBSWebsocketDotNet.Types;

namespace MacroPad.Plugins.Nodes.OBS.Nodes
{
    internal class SetCurrentScene : INodeRunner
    {
        public string Name => "Set Current Scene";

        public string Description => "Set the program scene";

        public string Id => "SetCurrentScene";

        public TypeNamePair[] Inputs => [new(typeof(SceneBasicInfo),"Scene")];

        public TypeNamePair[] Outputs => [];

        public int RunnerOutputCount => 1;

        public string[] RunnerOutputsName => [];

        public INodeComponent[] Components => [];

        public bool IsVisible(IDeviceLayoutButton button, IDeviceOutput output) => true;

        public NodeRunnerResult Run(IResourceManager r)
        {
            OBSProtocol.Run(x => x.SetCurrentProgramScene(((SceneBasicInfo)r.GetValue(0)).Name));

            return new NodeRunnerResult { RunnerOutputIndex = 0, Results = [] };
        }
    }
}
