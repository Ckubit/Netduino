using System;
using Microsoft.SPOT;

namespace NetduinoLibrary.Actuators
{
    /// <summary>
    /// Definition for a servo motor.
    /// </summary>
    public struct ServoMotorDefinition
    {
        /// <summary>
        /// Default period length expressed microseconds.
        /// </summary>
        public const uint DefaultPeriod = 20 * 1000;
        /// <summary>
        /// Minimum duration expressed microseconds that servo supports.
        /// </summary>
        public uint MinimumDuration;
        /// <summary>
        /// Maximum duration expressed microseconds that servo supports.
        /// </summary>
        public uint MaximumDuration;
        /// <summary>
        /// Period length expressed microseconds.
        /// </summary>
        public uint Period;

        /// <summary>
        /// Initializes a new instance of the ServoMotorDefinition class. This constructors uses the <see cref="DefaultPeriod"/>.
        /// </summary>
        /// <param name="minimumDuration">Minimum duration of wave [microseconds]</param>
        /// <param name="maximumDuration">Maximum duration of wave [microseconds]</param>
        public ServoMotorDefinition(uint minimumDuration, uint maximumDuration)
            : this(minimumDuration, maximumDuration, DefaultPeriod)
        {}
        /// <summary>
        /// Initializes a new instance of the ServoMotorDefinition class.
        /// </summary>
        /// <param name="minimumDuration">Minimum duration of wave [microseconds]</param>
        /// <param name="maximumDuration">Maximum duration of wave [microseconds]</param>
        /// <param name="period">Period length expressed of wave [microseconds]</param>
        public ServoMotorDefinition(uint minimumDuration, uint maximumDuration, uint period)
        {
            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
            Period = period;
        }
    }
}
