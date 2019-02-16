using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
namespace Assets.Scripts.Utilities.Timers
{
    /// <summary>
    /// Just a simple wrapper class for C# Timers.
    /// </summary>
    public class CSTimer
    {
        /// <summary>
        /// The actual c# timer.
        /// </summary>
        public Timer timer;

        /// <summary>
        /// Used to keep track of the values set for the timer so that it can be reset.
        /// </summary>
        private int _numberOfMilliseconds;
        private bool _autoRestart;
        private ElapsedEventHandler _event;

        /// <summary>
        /// Property to wrap number of milliseconds for this timer.
        /// </summary>
        public int numberofMilliseconds
        {
            get
            {
                return _numberOfMilliseconds;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="NumberOfMilliseconds">Number of milliseconds until the timer finishes.</param>
        /// <param name="AutoRestart">Should the timer loop?</param>
        /// <param name="OnFinished">What happens when the timer finishes.</param>
        public CSTimer(int NumberOfMilliseconds,bool AutoRestart,ElapsedEventHandler OnFinished)
        {
            timer = new Timer(NumberOfMilliseconds);
            timer.Elapsed += OnFinished;
            timer.AutoReset = AutoRestart;
            this._numberOfMilliseconds = NumberOfMilliseconds;
            this._autoRestart = AutoRestart;
            this._event = OnFinished;
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public void start()
        {
            timer.Start();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// Restart the timer and make it enabled.
        /// </summary>
        public void restart()
        {
            timer = new Timer(numberofMilliseconds);
            timer.Elapsed += _event;
            timer.AutoReset = _autoRestart;
            timer.Start();
        }

        /// <summary>
        /// Reset the timer but leave it disabled.
        /// </summary>
        public void reset()
        {
            timer = new Timer(numberofMilliseconds);
            timer.Elapsed += _event;
            timer.AutoReset = _autoRestart;
        }

    }
}
