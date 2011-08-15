using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;

namespace NetduinoLibrary.Actuators
{
    /// <summary>
    /// Controls the servo motor by using Netduino's PWM capability.
    /// </summary>
    public class ServoMotor: DisposableObject
    {
        /// <summary>
        /// Maximum angle.
        /// </summary>
        private const float AngleRange = 180.0f;
        /// <summary>
        /// PWM instance that communicates with servor motor.
        /// </summary>
        private PWM pwm;
        
        private float angle;
        /// <summary>
        /// Servo motor definition.
        /// </summary>
        private ServoMotorDefinition definition;
        /// <summary>
        /// The duration per angle's degree.
        /// </summary>
        private double rangePerDegree;
        /// <summary>
        /// A flag that signals whether servo motor has been moved. When false setting the Angle property will always move the motor,
        /// otherwise the motor will move only when new angle is different than the actual one.
        /// </summary>
        /// <remarks>Motor won't move until Angle is set explicitly.</remarks>
        private bool isMoved;

        public ServoMotor(Cpu.Pin pin, ServoMotorDefinition definition)
        {
            pwm = new PWM(pin);
            this.definition = definition;
            UpdateRange();
        }

        /// <summary>
        /// Current angle.
        /// </summary>
        /// <remarks>Motor won't move until Angle is set explicitly.</remarks>
        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                float newAngle;
                if (value > AngleRange)
                    newAngle = AngleRange;
                else if (value < 0)
                    newAngle = 0;
                else
                    newAngle = value;
                if (newAngle != Angle || !isMoved)
                {
                    angle = newAngle;
                    Rotate();
                    isMoved = true;
                }
            }
        }

        /// <summary>
        /// Rotate the motor to current <see cref="Angle"/>.
        /// </summary>
        public void Rotate()
        {
            uint duration = (uint)(definition.MinimumDuration + rangePerDegree * Angle);
            pwm.SetPulse(definition.Period, duration);
        }

        /// <summary>
        /// Updates the temporary variables when definition changes.
        /// </summary>
        private void UpdateRange()
        {
            rangePerDegree = (definition.MaximumDuration - definition.MinimumDuration) / AngleRange;
        }

        /// <summary>
        /// Minimum duration expressed microseconds that servo supports.
        /// </summary>
        public uint MinimumDuration
        {
            get { return definition.MinimumDuration; }
            set
            {
                if (definition.MinimumDuration != value)
                {
                    definition.MinimumDuration = value;
                    UpdateRange();
                }
            }
        }

        /// <summary>
        /// Maximum duration expressed microseconds that servo supports.
        /// </summary>
        public uint MaximumDuration
        {
            get { return definition.MaximumDuration; }
            set
            {
                if (definition.MaximumDuration != value)
                {
                    definition.MaximumDuration = value;
                    UpdateRange();
                }
            }
        }

        /// <summary>
        /// Period length expressed microseconds.
        /// </summary>
        public uint Period
        {
            get { return definition.Period; }
            set
            {
                definition.Period = value;
                if (isMoved)
                    Rotate();
            }
        }
        
        /// <summary>
        /// Disposes managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pwm.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
