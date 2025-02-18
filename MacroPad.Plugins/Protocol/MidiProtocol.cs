using Commons.Music.Midi;
using Commons.Music.Midi.Alsa;
using Commons.Music.Midi.PortMidi;
using Commons.Music.Midi.RtMidi;
using Commons.Music.Midi.WinMM;
using MacroPad.Shared.Plugin;
using MacroPad.Shared.Plugin.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Protocol.Midi.Protocol
{
    internal class MidiProtocol : IProtocol
    {
        public string Name => "MIDI Protocol";
        public static string ProtocolId => "MacroPad.Plugins.Protocol.Midi.MidiProtocol";
        public string Id => ProtocolId;

        private IMidiAccess? _midiAccess = null;//(RtMidiAccess)MidiAccessManager.Default;
        private HashSet<string> _lastPorts = [];
        private HashSet<MidiDevice> _connectedDevices = [];

        public event EventHandler<DeviceDetectedEventArgs>? DeviceDetected;
        public event EventHandler<DeviceDetectedEventArgs>? DeviceDisconnected;

        private CancellationTokenSource? _enabled = null;

        public void Enable()
        {
            if (_enabled != null) throw new Exception("Can't enable an already enabled protocol.");
            _enabled = new CancellationTokenSource();
            _midiAccess =
                OperatingSystem.IsWindows() ? new WinMMMidiAccess() : 
                OperatingSystem.IsLinux() ? new AlsaMidiAccess() : new PortMidiAccess();
            _ = DetectDeviceLoop();
        }
        public void Disable()
        {
            if (_enabled == null) return;
            _enabled.Cancel();
            _enabled = null;
            _lastPorts = [];
            foreach (MidiDevice device in _connectedDevices)
            {
                DeviceDisconnected?.Invoke(this, new DeviceDetectedEventArgs(device));
                device.Disconnect();
            }
            _connectedDevices = [];
            _midiAccess = null;
        }
        private async Task DetectDeviceLoop()
        {
            while (!(_enabled == null || _enabled.IsCancellationRequested))
            {
                if (_midiAccess == null) throw new Exception("No midi access.");
                try
                {
                    HashSet<IMidiPortDetails> newPorts = _midiAccess.Inputs.ToHashSet();



                    _connectedDevices = _connectedDevices.Where(device =>
                    {
                        bool isConnected = newPorts.Any(x => x.Id == device.InputDevice.Id && x.Name == device.InputDevice.Name);
                        if (!isConnected)
                        {
                            DeviceDisconnected?.Invoke(this, new DeviceDetectedEventArgs(device));
                        }
                        return isConnected;
                    }).ToHashSet();

                    foreach (IMidiPortDetails port in newPorts)
                    {
                        if (!_lastPorts.Contains(port.Id))
                        {
                            List<IMidiPortDetails> outputPorts = _midiAccess.Outputs.Where((x) => x.Name == port.Name && x.Manufacturer == port.Manufacturer).ToList();
                            outputPorts.Sort((IMidiPortDetails p1, IMidiPortDetails p2) => (int.Parse(port.Id) - int.Parse(p1.Id)).CompareTo(int.Parse(port.Id) - int.Parse(p2.Id)));

                            MidiDevice newDevice = new MidiDevice(port, outputPorts.Count >= 1 ? outputPorts[0] : null, _midiAccess);
                            _connectedDevices.Add(newDevice);

                            DeviceDetected?.Invoke(this, new DeviceDetectedEventArgs(newDevice));
                        }
                    }

                    _lastPorts = newPorts.Select((x) => x.Id).ToHashSet();

                    await Task.Delay(5000, _enabled.Token).ContinueWith((t) => { });
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
