using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities.Timers
{
    /// <summary>
    /// Class that manages cooldowns with real time.
    /// </summary>
    public class TimedCooldown:CooldownBase
    {
        /// <summary>
        /// The C# timer that controls the class.
        /// </summary>
        public CSTimer timer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NumberOfMilliseconds">The number of milliseconds for the timer until it expires.</param>
        /// <param name="Value">The value for the cooldown.</param>
        /// <param name="DecrementAmount">The value to decrement the cooldown each time the timer expires.</param>
        public TimedCooldown(int NumberOfMilliseconds,double Value, double DecrementAmount): base(Value, DecrementAmount)
        {
            this.timer = new CSTimer(NumberOfMilliseconds, true, DecrementValue);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Timer">The c# timer that controls the cooldown.</param>
        /// <param name="Value">The max value for the cooldown.</param>
        /// <param name="DecrementAmount">The value to decrement the cooldown each time the timer expires.</param>
        public TimedCooldown(CSTimer Timer,double Value, double DecrementAmount) : base(Value, DecrementAmount)
        {
            this.timer = Timer;
            this.timer.timer.Elapsed += DecrementValue;
        }

        /// <summary>
        /// Decrement the actual cooldown value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecrementValue(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.decrementCoolDown();
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public void start()
        {
            this.timer.start();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void stop()
        {
            this.timer.stop();
        }

        /// <summary>
        /// Restart the cooldown and start counting down.
        /// </summary>
        public void restart()
        {
            this.timer.restart();
        }

        /// <summary>
        /// Reset the cooldown but leave it disabled.
        /// </summary>
        public void reset()
        {
            this.timer.reset();
        }

    }
}
