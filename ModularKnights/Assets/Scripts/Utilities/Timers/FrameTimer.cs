using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utilities.Delegates;
using TimerState = Assets.Scripts.Enums.TimerState; //Alias.

namespace Assets.Scripts.Utilities.Timers
{

    /// <summary>
    /// A timer that ticks down every time update is called.
    /// Note, must call timer.Update in an appropriate other script since this does not derive from MonoBehavior.
    /// </summary>
    public class FrameTimer
    {
        /// <summary>
        /// The number of frames (update ticks) remaining until this timer expires.
        /// </summary>
        public int lifespanRemaining;
        /// <summary>
        /// The max amount of frames this timer has been assigned.
        /// </summary>
        public int maxLifespan;
        /// <summary>
        /// Should the timer restart itself when it counts down to 0.
        /// </summary>
        public bool autoRestart;
        /// <summary>
        /// The function that is called when the timer hits zero frames remaining.
        /// </summary>
        public VoidDelegate onFinished;


        /// <summary>
        /// The current state of the timer.
        /// </summary>
        private TimerState currentState;


        /// <summary>
        /// Checks if the timer has been initialized, but not yet ticking.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.currentState == TimerState.Initialized;
            }
        }

        /// <summary>
        /// Checks if the timer is currently ticking.
        /// </summary>
        public bool IsTicking
        {
            get
            {
                return this.currentState == TimerState.Ticking;
            }
        }

        /// <summary>
        /// Checks if the timer was paused. This means the timer could be resumed at any moment.
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return this.currentState == TimerState.Paused;
            }
        }

        /// <summary>
        /// Checks if the timer was stopped, aka it no longer needed to run but it's finish state was not invoked.
        /// </summary>
        public bool IsStopped
        {
            get
            {
                return this.currentState == TimerState.Stopped;
            }
        }

        /// <summary>
        /// Checks if the timer has finished running it's course.
        /// </summary>
        public bool IsFinished
        {
            get
            {
                return this.finished();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="LifeSpan">How many frames aka update ticks should this timer exist for.</param>
        public FrameTimer(int LifeSpan,VoidDelegate OnFinished=null,bool AutoRestart=false)
        {
            this.maxLifespan = LifeSpan;
            this.lifespanRemaining = this.maxLifespan;
            this.autoRestart = AutoRestart;
            this.onFinished = OnFinished;
            this.currentState = TimerState.Initialized;
        }

        /// <summary>
        /// Starts the timer ticking.
        /// </summary>
        public void start()
        {
            this.currentState = TimerState.Ticking;
        }

        /// <summary>
        /// Ticks the timer's remaining lifespan down one frame.
        /// </summary>
        public void tick()
        {
            if (this.currentState != TimerState.Ticking) return; //If the timer isn't supposed to tick do nothing.

            if (lifespanRemaining >= 0)
            {
                lifespanRemaining--;
                if (finished())
                {
                    this.currentState = TimerState.Finished;
                    invoke();
                    if (autoRestart) restart();
                }
            }
            else return;
        }

        /// <summary>
        /// Checks if the timer has finished running.
        /// </summary>
        /// <returns></returns>
        public bool finished()
        {
            return this.lifespanRemaining == 0;
        }

        /// <summary>
        /// Wrapper for tick(); Does the exact same thing.
        /// </summary>
        public void Update()
        {
            tick();
        }

        /// <summary>
        /// Calls whatever function is associated with this timer when it has expired.
        /// </summary>
        public void invoke()
        {
            if (this.onFinished != null)
            {
                this.onFinished.Invoke();
            }
        }

        /// <summary>
        /// Restarts the frame timer.
        /// </summary>
        public void restart()
        {
            lifespanRemaining = maxLifespan;
            this.currentState = TimerState.Ticking;
        }

        /// <summary>
        /// Stops the timer from executing it's count down.
        /// </summary>
        public void stop()
        {
            lifespanRemaining = -1;
            this.currentState = TimerState.Stopped;
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void pause()
        {
            this.currentState = TimerState.Paused;
        }

        /// <summary>
        /// Resumes the timer if it has been paused.
        /// </summary>
        public void resume()
        {
            if (this.currentState == TimerState.Paused)
            {
                this.currentState = TimerState.Ticking;
            }
        }

    }
}
