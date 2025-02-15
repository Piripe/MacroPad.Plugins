using Commons.Music.Midi;
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

        private IMidiAccess _midiAccess = new WinMMMidiAccess();//(RtMidiAccess)MidiAccessManager.Default;
        private List<string> _lastPorts = new List<string>();
        private List<MidiDevice> _connectedDevices = new List<MidiDevice>();

        public event EventHandler<DeviceDetectedEventArgs>? DeviceDetected;
        public event EventHandler<DeviceDetectedEventArgs>? DeviceDisconnected;

        public void Enable()
        {
            
            _ = DetectDeviceLoop();
        }
        private async Task DetectDeviceLoop()
        {
            while (true)
            {
                try
                {
                    List<IMidiPortDetails> newPorts = _midiAccess.Inputs.ToList();



                    _connectedDevices = _connectedDevices.FindAll(device =>
                    {
                        bool isConnected = newPorts.Any(x => x.Id == device.InputDevice.Id && x.Name == device.InputDevice.Name);
                        if (!isConnected)
                        {
                            DeviceDisconnected?.Invoke(this, new DeviceDetectedEventArgs(device));
                        }
                        return isConnected;
                    });

                    newPorts.ForEach(port =>
                    {
                        if (!_lastPorts.Contains(port.Id))
                        {
                            List<IMidiPortDetails> outputPorts = _midiAccess.Outputs.Where((x) => x.Name == port.Name && x.Manufacturer == port.Manufacturer).ToList();
                            outputPorts.Sort((IMidiPortDetails p1, IMidiPortDetails p2) => (int.Parse(port.Id) - int.Parse(p1.Id)).CompareTo(int.Parse(port.Id) - int.Parse(p2.Id)));

                            MidiDevice newDevice = new MidiDevice(port, outputPorts.Count >= 1 ? outputPorts[0] : null, _midiAccess);
                            _connectedDevices.Add(newDevice);

                            DeviceDetected?.Invoke(this, new DeviceDetectedEventArgs(newDevice));
                        }
                    });

                    _lastPorts = newPorts.Select((x) => x.Id).ToList();

                    await Task.Delay(5000);
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
