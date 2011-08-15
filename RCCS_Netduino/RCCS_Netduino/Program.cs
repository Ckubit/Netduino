/*
 * Traxxas XL5 series Example Code
 *	Coded by Chris Seto August 2010
 *	<chris@chrisseto.com> 
 *	
 * Use this code for whatever you want. Modify it, redistribute it, I don't care.
 * I do ask that you please keep this header intact, however.
 * If you modfy the driver, please include your contribution below:
 * 
 * Chris Seto: Inital release (1.0)
 * Chris Seto: 1.1 release (new RC drive demo, code cleanup)
 * Chris Seto: Netduino port (1.1 -> Netduino branch)
 * 
 * */

using System.Threading;
using ElectricSpeedController_API;
using PPM_Decoder_API;
using SecretLabs.NETMF.Hardware.Netduino;
using Servo_API;

namespace RCCS_Netduino
{
	public class Program
	{
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
		/// Throttle channel in from the RX
		/// </summary>
		private static PPM_Channel throttleChannel;

		/// <summary>
		/// Steering channel in from the RX
		/// </summary>
		private static PPM_Channel steeringChannel;

		/// <summary>
		/// ESC speed limit for the RC control loop
		/// </summary>
		private const int speedLimit = 100;

		public static void Main()
		{
			// Setup steering servo
			steering = new Servo(Pins.GPIO_PIN_D9);

			// Setup ESC
			xl5 = new SpeedController(Pins.GPIO_PIN_D10, new TRAXXAS_XL5());
			xl5.DriveMode = SpeedController.DriveModes.Forward;

			// Setup PPM decoder channels
			//throttleChannel = new PPM_Channel(Pins.GPIO_PIN_D7);
			//steeringChannel = new PPM_Channel(Pins.GPIO_PIN_D6);

			// Set the servo channel to proper output scaling 
			//steeringChannel.calibrateOutput(0, 180);

			// Sweep the wheels from left to right
			for (int i = 0; i <= 30; i++)
			{
				steering.Degree = i * 6;
				Thread.Sleep(30);
			}

			// Center steering
			steering.Degree = 90.5;

			// Switch to RC control mode
			while (true)
			{
				// Set the steering servo to the PPM wave
				//steering.Degree = steeringChannel.Read;

                steering.Degree = 0;

				// Get the throttle input
				//int throttleIn = throttleChannel.Read;
                int throttleIn = 50;
              

				// Figure out which direction we are going
				if (throttleIn >= 0)
					xl5.DriveMode = SpeedController.DriveModes.Forward;
				else
				{
					// Set ESC to reverse
					//xl5.DriveMode = SpeedController.DriveModes.Reverse;

					// Flip the negative to a positive
					//throttleIn = throttleIn * -1;

					// For now, let's just pull the throttle all the way out
					xl5.Throttle = 0;
				}

				// Are we going too fast?
				if (throttleIn > speedLimit)
					throttleIn = speedLimit;

				// Set the throttle
				xl5.Throttle = throttleIn;
               
			}
		}
	}
}
