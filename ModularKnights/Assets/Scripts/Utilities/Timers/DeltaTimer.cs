using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TimerType = Assets.Scripts.Enums.TimerType;
using TimerState = Assets.Scripts.Enums.TimerState;
using Assets.Scripts.Utilities.Delegates;

namespace Assets.Scripts.Utilities.Timers
{
    /// <summary>
    /// Experimental timer class which uses Unity's delta time. Make sure to call this timer's update function in an appropriate monobehavior script!
    /// </summary>
    [SerializeField,Serializable]
    public class DeltaTimer
    {
        /// <summary>
        /// The current time on the timer.
        /// </summary>
        public decimal currentTime;
        /// <summary>
        /// The time (in seconds) it should take this timer to tick to completion. Note it is a float so you can have fractions of a second.
        /// </summary>
        public decimal maxTime;

        /// <summary>
        /// The type of timer this is.
        /// </summary>
        public TimerType type;
        /// <summary>
        /// The current state of the timer.
        /// </summary>
        public TimerState state;

        /// <summary>
        /// What happens when the timer finishes.
        /// </summary>
        public Assets.Scripts.Utilities.Delegates.VoidDelegate onFinished;

        /// <summary>
        /// Does the timer automatically restart?
        /// </summary>
        public bool autoRestart;

        /// <summary>
        /// Gets hours 
        /// </summary>
        public int hours
        {
            get
            {
                return (int)(currentTime / 3600);
            }
        }

        public int minutes
        {
            get
            {
                return (int)(currentTime / 60);
            }
        }

        public int seconds
        {
            get
            {
                return (int)(currentTime % 60);
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="TimeToCompletion">How long it takes in seconds until the timer finishes.</param>
        /// <param name="Type">The type of timer this is.</param>
        /// <param name="AutoRestart">If the timer should automatically restart once it finishes.</param>
        /// <param name="OnFinished">What happens when the timer finishes.</param>
        public DeltaTimer(decimal TimeToCompletion,TimerType Type,bool AutoRestart, VoidDelegate OnFinished=null)
        {
            this.type = Type;
            this.autoRestart = AutoRestart;
            if(Type == TimerType.CountDown)
            {
                this.currentTime = TimeToCompletion;
            }
            else if( Type== TimerType.CountUp)
            {
                this.currentTime = TimeToCompletion;
            }
            this.maxTime = TimeToCompletion;
            this.onFinished = OnFinished;
        }

        /// <summary>
        /// If the timer is initialized;
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.state == TimerState.Initialized;
            }
        }

        /// <summary>
        /// If the timer is ticking.
        /// </summary>
        public bool IsTicking
        {
            get
            {
                return this.state == TimerState.Ticking;
            }
        }

        /// <summary>
        /// If the timer is paused.
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return this.state == TimerState.Paused;
            }
        }

        /// <summary>
        /// If the timer is stopped.
        /// </summary>
        public bool IsStopped
        {
            get
            {
                return this.state == TimerState.Stopped;
            }
        }

        /// <summary>
        /// If the timer is finished.
        /// </summary>
        public bool IsFinished
        {
            get
            {
                return this.state == TimerState.Finished;
            }
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public void start()
        {
            this.state = TimerState.Ticking;
        }
        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void stop()
        {
            this.currentTime = -1;
            this.state = TimerState.Stopped;
        }

        /// <summary>
        /// Pause the timer.
        /// </summary>
        public void pause()
        {
            this.state = TimerState.Paused;
        }
        /// <summary>
        /// Resume the timer.
        /// </summary>
        public void resume()
        {
            this.state = TimerState.Ticking;
        }

        /// <summary>
        /// Restart the timer.
        /// </summary>
        public void restart()
        {
            if (type == TimerType.CountDown)
            {
                currentTime = maxTime;
            }
            else if (type == TimerType.CountUp)
            {
                currentTime = 0;
            }
            this.state = TimerState.Ticking;
        }

        /// <summary>
        /// Tick aka update the timer.
        /// </summary>
        public void tick()
        {
            Update();
        }

        /// <summary>
        /// Update the timer.
        /// </summary>
        public void Update()
        {
            if (state != TimerState.Ticking) return; //Only update if timer should tick.

            if (type == TimerType.CountUp)
            {
                currentTime +=(decimal)Time.deltaTime;
                if (currentTime >= maxTime)
                {
                    //do something
                    invoke();
                    state = TimerState.Finished;
                    if (autoRestart == true) restart();
                }
            }
            else if(type== TimerType.CountDown)
            {
                currentTime -= (decimal)Time.deltaTime;
                if (currentTime <= 0)
                {
                    //do something.
                    invoke();
                    state = TimerState.Finished;
                    if (autoRestart == true) restart();
                }
            }
        }

        /// <summary>
        /// invoke the timer's functionality upon finish.
        /// </summary>
        private void invoke()
        {
            if (this.onFinished == null) return;
            else onFinished.Invoke();
        }

    }
}
