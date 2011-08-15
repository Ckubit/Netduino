using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO;

//RC
using ElectricSpeedController_API;
//using PPM_Decoder_API;
using Servo_API;

//using NetduinoLibrary.Actuators;

namespace NetduinoPlusWebServer
{
    public class Program
    {
        //Webserver constant
            const string WebFolder = "\\SD\\Web";

        //RC constants
            /// <summary>
            /// Steering servo
            /// (Traxxas 2056)
            /// </summary>
            private static Servo steering;

            /// <summary>
            /// ESC
            /// (Traxxas XL-5)
            /// </summary>
            private static SpeedController xl5;

            /// <summary>
            /// ESC speed limit for the RC control loop
            /// </summary>
            private const int speedLimit = 100;


        //END RC constants

        public static void Main()
        {

            //RC stuff

                // Setup steering servo
                steering = new Servo(Pins.GPIO_PIN_D9);

                // Setup ESC
                xl5 = new SpeedController(Pins.GPIO_PIN_D10, new TRAXXAS_XL5());
                xl5.DriveMode = SpeedController.DriveModes.Forward;

            //End RC stuff


            Listener webServer = new Listener(RequestReceived);
 
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            while (true)
            {
                // Blink LED to show we're still responsive
                led.Write(!led.Read());
                Thread.Sleep(500);
            }

        }


        private static void RequestReceived(Request request)
        {
            // Use this for a really basic check that it's working
            //request.SendResponse("<html><body><p>Request from " + request.Client.ToString() + " received at " + DateTime.Now.ToString() + "</p><p>Method: " + request.Method + "<br />URL: " + request.URL +"</p></body></html>");

            // sent to output
            if (request.URL.ToString() == "/forward" || request.URL.ToString().ToString() == "/test.html") 
            {
                //TrySendFile(request);

                //OutputPort D0 = new OutputPort(Pins.GPIO_PIN_D0, false);
                //D0.Write(true);
                //Thread.Sleep(500);
                //D0.Write(false);
                //Thread.Sleep(500);
                //D0.Dispose();


                    // Sweep the wheels from left to right
                    for (int i = 0; i <= 30; i++)
                    {
                        steering.Degree = i * 6;
                        Thread.Sleep(30);
                    }

                    // Center steering
                    steering.Degree = 90.5;

                    xl5.DriveMode = SpeedController.DriveModes.Forward;

                    for (int j = 50; j >= 0; j--)
                    {
                        xl5.Throttle = j;
                        Thread.Sleep(30);
                    }
                
                    //Thread.Sleep(10000);
                    //xl5.Throttle = 0;
                    request.SendResponse("<html><body><p>FORWARD<BR><BR>Request from " + request.Client.ToString() + " received at " + DateTime.Now.ToString() + "</p><p>Method: " + request.Method + "<br />URL: " + request.URL + "</p></body></html>");
            }
            else if (request.URL.ToString() == "/backward" || request.URL.ToString().ToString() == "/test2.html") 
            {


                // Sweep the wheels from left to right
                for (int i = 0; i <= 30; i++)
                {
                    steering.Degree = i * 6;
                    Thread.Sleep(30);
                }

                // Center steering
                steering.Degree = 90.5;
                xl5.DriveMode = SpeedController.DriveModes.Reverse;
                for (int j = 50; j >= 0; j--)
                {
                    xl5.Throttle = j;
                    Thread.Sleep(30);
                }


                request.SendResponse("<html><body><p>BACKWARD<BR><BR>Request from " + request.Client.ToString() + " received at " + DateTime.Now.ToString() + "</p><p>Method: " + request.Method + "<br />URL: " + request.URL + "</p></body></html>");
                //TrySendFile(request);
                //OutputPort D0 = new OutputPort(Pins.GPIO_PIN_D0, false);
                //D0.Write(true);
                //Thread.Sleep(1500);
                //D0.Write(false);
                //Thread.Sleep(1500);
                //D0.Dispose();
            }
            else
            {
                request.SendResponse("<html><body><p>UNKNOWN COMMAND<BR><BR>Request from " + request.Client.ToString() + " received at " + DateTime.Now.ToString() + "</p><p>Method: " + request.Method + "<br />URL: " + request.URL + "</p></body></html>");
            }


            // Send a file
            //TrySendFile(request);

        }

        /// <summary>
        /// Look for a file on the SD card and send it back if it exists
        /// </summary>
        /// <param name="request"></param>
        private static void TrySendFile(Request request)
        {
            // Replace / with \
            string filePath = WebFolder + request.URL.Replace('/', '\\');

            if (File.Exists(filePath))
                request.SendFile(filePath);
            else
                request.Send404();
        }

    }
}
