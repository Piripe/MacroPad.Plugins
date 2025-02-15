using Commons.Music.Midi;
using Commons.Music.Midi.RtMidi;
using MacroPad.Plugins.Protocol.Midi.Protocol;
using MacroPad.Shared.Plugin.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Protocol.Midi.Protocol
{
    internal class MidiDevice : IProtocolDevice
    {
        public string Name { get; }
        public string Id { get; }

        public string Protocol => MidiProtocol.ProtocolId;

        public bool IsConnected => _input != null;

        public event EventHandler<DeviceInputEventArgs>? DeviceInput;

        internal IMidiPortDetails InputDevice { get; }
        internal IMidiPortDetails? OutputDevice { get; }

        private IMidiInput? _input;
        private IMidiOutput? _output;
        private IMidiAccess _access;

        public async void Connect()
        {
            try
            {
                _input = await _access.OpenInputAsync(InputDevice.Id);

                _input.MessageReceived += _input_MessageReceived;

                if (OutputDevice != null)
                {
                    _output = await _access.OpenOutputAsync(OutputDevice.Id);
                }
            } catch {

            }
        }

        private void _input_MessageReceived(object? sender, MidiReceivedEventArgs e)
        {
            if (e.Data[0] == MidiEvent.NoteOff)
            {
                e.Data[0] = MidiEvent.NoteOn;
                e.Data[2] = 0;
            }
            DeviceInput?.Invoke(this, new DeviceInputEventArgs((e.Data[0] << 8) + e.Data[1], e.Data[2]/127f));
        }

        public async void Disconnect()
        {
            try
            {
                if (_output != null) await _output.CloseAsync();
                if (_input != null) await _input.CloseAsync();
            } catch {
            }
            _input = null;
            _output = null;
        }

        public void SetButtonRGB(int button, int color)
        {
            throw new NotImplementedException();
        }

        public void SetButtonPalette(int button, int color)
        {
            if (_output != null)
            {
                try
                {
                    _output.Send([(byte)(button>>8), (byte)(button&0xFF), (byte)color],0,3,0);
                } catch {}
            }
        }

        public void SetButtonImage(int button, object image)
        {
            throw new NotImplementedException();
        }

        public MidiDevice(IMidiPortDetails inputDevice, IMidiPortDetails? outputDevice, IMidiAccess access) {
            Name = inputDevice.Name;
            Id = inputDevice.Manufacturer + "." + inputDevice.Name;

            InputDevice = inputDevice;
            OutputDevice = outputDevice;
            _access = access;
        }
    }
}
