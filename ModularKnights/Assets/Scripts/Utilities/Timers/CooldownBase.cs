using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities.Timers
{
    /// <summary>
    /// A base class for cooldowns.
    /// </summary>
    public class CooldownBase
    {
        /// <summary>
        /// The max value for the cooldown.
        /// </summary>
        public double value;
        /// <summary>
        /// The amount to decrement for the cooldown each time a timer goes off.
        /// </summary>
        public double decrementAmount;

        public double timeRemaining
        {
            get
            {
                double rounded = System.Math.Round(value);
                return rounded;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Value">The max value for the cooldown.</param>
        /// <param name="DecrementAmount">The amount to decrement for the cooldown each time a timer goes off.</param>
        public CooldownBase(double Value, double DecrementAmount)
        {
            this.value = Value;
            this.decrementAmount = DecrementAmount;
        }


        /// <summary>
        /// Decrement the cooldown each timer a timer goes off.
        /// </summary>
        public virtual void decrementCoolDown()
        {
            if (this.value <= 0) return;
            this.value -= this.decrementAmount;
            if (this.value <= 0) this.value = 0.0;
        }

        /// <summary>
        /// Decrement the cooldown timer by a certain amount.
        /// </summary>
        /// <param name="Value">The amount to decrement the value by.</param>
        public virtual void decrementCoolDown(double Value)
        {
            if (this.value <= 0) return;
            this.value -= Value;
            if (this.value <= 0) this.value = 0.0;
        }

        /// <summary>
        /// Make the cooldown ready by setting the value to 0.0;
        /// </summary>
        public virtual void makeReady()
        {
            this.value = 0.0;
        }

        /// <summary>
        /// Checks if the cooldown is ready.
        /// </summary>
        /// <returns></returns>
        public virtual bool isReady()
        {
            return this.value <= 0;
        }

        /// <summary>
        /// Override this.
        /// </summary>
        public virtual void tick()
        {

        }

    }
}
