using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

/* NOTE: make sure you change the deployment target from the Emulator to your Netduino before running this
 * Netduino sample app.  To do this, select "Project menu > ButtonApp Properties > .NET Micro Framework" and 
 * then change the Transport type to USB.  Finally, close the ButtonApp properties tab to save these settings. */

namespace ButtonApp
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            InputPort button = new InputPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled);
            bool buttonState = false;

            while (true)
            {
                buttonState = button.Read();
                led.Write(!buttonState);
                //Thread.Sleep(700);
                //led.Write(buttonState);
               
            }

        }

    }
}
