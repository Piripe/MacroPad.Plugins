using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Components;
using OBSWebsocketDotNet.Types;

namespace MacroPad.Plugins.Nodes.OBS
{
    public static class NodeTypes
    {
        public static NodeType obsSceneType = new NodeType(
                "OBS Scene",
                new Shared.Media.Color(89, 53, 178),
                typeof(SceneBasicInfo),
                new SceneBasicInfo(),
                x => x switch {
                    SceneBasicInfo scene => scene,
                    string sceneName => OBSProtocol.GetObsScene(sceneName),
                    decimal sceneIndex => OBSProtocol.Scenes[(int)sceneIndex],
                    _ => null
                },
                r => OBSProtocol.GetObsScene(r.GetData<string>("d") ?? ""),
                [
                    new ComboBox()
                    {
                        GetSelectedItem = (r) => OBSProtocol.GetObsScene(r.GetData<string>("d") ?? "")?.Name ?? "",
                        SelectionChanged = (r, i) => r.SetData("d", OBSProtocol.Scenes[i].Name)
                    }
                ]
            );
    }
}
