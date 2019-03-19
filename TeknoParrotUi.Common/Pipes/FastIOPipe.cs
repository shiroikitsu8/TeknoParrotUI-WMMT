﻿using System;
using System.IO.Pipes;
using System.Threading;

namespace TeknoParrotUi.Common.Pipes
{
    public class FastIOPipe : ControlPipe
    {
        public override void Transmit()
        {
            while (true)
            {
                _npServer.WaitForConnection();

                try
                {
                    Thread.Sleep(15);
                    var report = GenButtonsFastIo();

                    _npServer.Write(report, 0, 64);
                    _npServer.Flush();
                    if (!_isRunning)
                        break;
                }
                catch (Exception)
                {
                    // In case pipe is broken
                    _npServer.Close();
                    _npServer = new NamedPipeServerStream(PipeName);
                    break;
                }

                if (!_isRunning)
                    break;
            }
        }

        private byte[] GenButtonsFastIo()
        {
            byte[] data = new byte[64];

            // Player 1
            if (InputCode.PlayerDigitalButtons[0].LeftPressed())
                data[1] |= 0x10;

            if (InputCode.PlayerDigitalButtons[0].RightPressed())
                data[1] |= 0x40;

            if (InputCode.PlayerDigitalButtons[0].DownPressed())
                data[1] |= 0x4;

            if (InputCode.PlayerDigitalButtons[0].UpPressed())
                data[1] |= 0x1;

            if (InputCode.PlayerDigitalButtons[0].Start != null && InputCode.PlayerDigitalButtons[0].Start.Value)
                data[0] |= 0x10;

            if (InputCode.PlayerDigitalButtons[0].Button1 != null && InputCode.PlayerDigitalButtons[0].Button1.Value)
                data[2] |= 0x1;

            if (InputCode.PlayerDigitalButtons[0].Button2 != null && InputCode.PlayerDigitalButtons[0].Button2.Value)
                data[2] |= 0x4;

            if (InputCode.PlayerDigitalButtons[0].Button3 != null && InputCode.PlayerDigitalButtons[0].Button3.Value)
                data[2] |= 0x10;

            if (InputCode.PlayerDigitalButtons[0].Button4 != null && InputCode.PlayerDigitalButtons[0].Button4.Value)
                data[2] |= 0x40;

            if (InputCode.PlayerDigitalButtons[0].Button5 != null && InputCode.PlayerDigitalButtons[0].Button5.Value)
                data[3] |= 0x1;

            if (InputCode.PlayerDigitalButtons[0].Button6 != null && InputCode.PlayerDigitalButtons[0].Button6.Value)
                data[3] |= 0x4;

            if (InputCode.PlayerDigitalButtons[0].Test != null && InputCode.PlayerDigitalButtons[0].Test.Value)
                data[0] |= 0x40;

            if (InputCode.PlayerDigitalButtons[0].Service != null && InputCode.PlayerDigitalButtons[0].Service.Value)
                data[0] |= 0x04;

            // Player 2
            if (InputCode.PlayerDigitalButtons[1].LeftPressed())
                data[1] |= 0x20;

            if (InputCode.PlayerDigitalButtons[1].RightPressed())
                data[1] |= 0x80;

            if (InputCode.PlayerDigitalButtons[1].DownPressed())
                data[1] |= 0x8;

            if (InputCode.PlayerDigitalButtons[1].UpPressed())
                data[1] |= 0x2;

            if (InputCode.PlayerDigitalButtons[1].Start != null && InputCode.PlayerDigitalButtons[1].Start.Value)
                data[0] |= 0x20;

            if (InputCode.PlayerDigitalButtons[1].Button1 != null && InputCode.PlayerDigitalButtons[1].Button1.Value)
                data[2] |= 0x2;

            if (InputCode.PlayerDigitalButtons[1].Button2 != null && InputCode.PlayerDigitalButtons[1].Button2.Value)
                data[2] |= 0x8;

            if (InputCode.PlayerDigitalButtons[1].Button3 != null && InputCode.PlayerDigitalButtons[1].Button3.Value)
                data[2] |= 0x20;

            if (InputCode.PlayerDigitalButtons[1].Button4 != null && InputCode.PlayerDigitalButtons[1].Button4.Value)
                data[2] |= 0x80;

            if (InputCode.PlayerDigitalButtons[1].Button5 != null && InputCode.PlayerDigitalButtons[1].Button5.Value)
                data[3] |= 0x2;

            if (InputCode.PlayerDigitalButtons[1].Button6 != null && InputCode.PlayerDigitalButtons[1].Button6.Value)
                data[3] |= 0x8;

            if (InputCode.PlayerDigitalButtons[1].Service != null && InputCode.PlayerDigitalButtons[1].Service.Value)
                data[0] |= 0x08;

            if (InputCode.PlayerDigitalButtons[0].Coin.HasValue && InputCode.PlayerDigitalButtons[0].Coin.Value)
                data[4] = 1;

            data[8] = InputCode.AnalogBytes[0];
            data[9] = InputCode.AnalogBytes[2];

            return data;
        }
    }
}