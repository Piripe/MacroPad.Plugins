using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Components;
using MacroPad.Shared.Plugin.Protocol;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

namespace MacroPad.Plugins.Nodes.OBS
{
    internal class OBSProtocol : IProtocol
    {
        public string Name => "OBS Protocol";

        public string Id => "MacroPad.Plugins.Nodes.OBS.OBSProtocol";

        public event EventHandler<DeviceDetectedEventArgs>? DeviceDetected;
        public event EventHandler<DeviceDetectedEventArgs>? DeviceDisconnected;

        public static OBSWebsocket? Client { get; private set; } = null;
        public static void Run(Action<OBSWebsocket> x)
        {
            if (Client != null && Client.IsConnected) try { x.Invoke(Client); } catch { }
        }
        public static List<SceneBasicInfo> Scenes = [];
        public static SceneBasicInfo? GetObsScene(string name) => Scenes.FirstOrDefault(x => x.Name == name);

        private CancellationTokenSource? _enabled = null;

        public void Enable()
        {
            if (Client != null && Client.IsConnected) return;

            if (_enabled != null) throw new Exception("Can't enable an already enabled protocol.");

            _enabled = new CancellationTokenSource();
            Client = new OBSWebsocket();

            Client.Connected += (s, e) =>
            {
                if (NodeTypes.obsSceneType.Components[0] is ComboBox sceneCB)
                    {
                        Scenes = Client.GetSceneList().Scenes;
                    sceneCB.Items.Clear();
                    foreach (var item in Scenes.Select(x => x.Name))
                        {
                            sceneCB.Items.Add(item);
                        }
                    }
                };
            Client.Disconnected += (s, e) =>
            {
                if (NodeTypes.obsSceneType.Components[0] is ComboBox sceneCB)
                {
                    sceneCB.Items.Clear();
                }

                if (_enabled != null)
                {
                    _enabled = new CancellationTokenSource();
                    _ = DetectOBSInstance();
                }
            };
            Client.SceneListChanged += (s, e) =>
            {
                if (NodeTypes.obsSceneType.Components[0] is ComboBox sceneCB)
                {
                    Scenes = Client.GetSceneList().Scenes;
                    sceneCB.Items.Clear();
                    foreach (var item in Scenes.Select(x => x.Name))
                    {
                        sceneCB.Items.Add(item);
                    }
                }
            };

            _ = DetectOBSInstance();
        }
        public void Disable()
        {
            if (Client == null || _enabled == null) return;
            _enabled.Cancel();
            _enabled = null;
            Client.Disconnect();
            Client = null;
        }

        private async Task DetectOBSInstance()
        {
            if (!(Client == null || _enabled == null || _enabled.IsCancellationRequested))
            {
                await Task.Delay(5000, _enabled.Token).ContinueWith(t => { });
                if (!(_enabled == null || _enabled.IsCancellationRequested)) Client.ConnectAsync("ws://127.0.0.1:4455", "");
            }
        }
    }
}
