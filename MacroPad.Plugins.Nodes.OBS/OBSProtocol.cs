using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Nodes.Components;
using MacroPad.Shared.Plugin.Protocol;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.OBS
{
    internal class OBSProtocol : IProtocol
    {
        public string Name => "OBS Protocol";

        public string Id => "MacroPad.Plugins.Nodes.OBS.OBSProtocol";

        public event EventHandler<DeviceDetectedEventArgs>? DeviceDetected;
        public event EventHandler<DeviceDetectedEventArgs>? DeviceDisconnected;

        public static OBSWebsocket? Client { get; private set; } = new OBSWebsocket();
        public static void Run(Action<OBSWebsocket> x)
        {
            if (Client != null && Client.IsConnected) x.Invoke(Client);
        }
        public static List<SceneBasicInfo> Scenes = [];
        public static SceneBasicInfo? GetObsScene(string name) => Scenes.FirstOrDefault(x => x.Name == name);

        public void Enable()
        {
            if (Client == null || Client.IsConnected) return;

            // Open WS
            Client.Connected += (s, e) =>
                {
                    if (NodeTypes.obsSceneType.Components[0] is ComboBox sceneCB)
                    {
                        Scenes = Client.GetSceneList().Scenes;
                        sceneCB.Items = Scenes.Select(x => x.Name).ToArray();
                    }
                };
            Client.SceneListChanged += (s, e) =>
            {
                Scenes = Client.GetSceneList().Scenes;
            };
            Client.ConnectAsync("ws://127.0.0.1:4455", "");

        }
        public void Disable()
        {
            if (Client == null) return;
            Client.Disconnect();
            Client = null;
        }

    }
}
